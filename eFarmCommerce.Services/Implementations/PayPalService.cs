using eFarmCommerce.Model.Exceptions;
using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Services.Database;
using eFarmCommerce.Services.Database.Entities;
using eFarmCommerce.Services.Interfaces;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace eFarmCommerce.Services.Implementations;

public class PayPalService : IPayPalService
{
    private readonly eFarmCommerceDbContext _context;
    private readonly IMapper _mapper;
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public PayPalService(
        eFarmCommerceDbContext context,
        IMapper mapper,
        HttpClient httpClient,
        IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<PayPalCreateOrderResponse> CreateOrderAsync(PayPalCreateOrderRequest request)
    {
        if (request.NarudzbaId == null && request.RezervacijaId == null)
            throw new ClientException("Plaćanje mora biti vezano za narudžbu ili rezervaciju.");

        if (request.NarudzbaId != null && request.RezervacijaId != null)
            throw new ClientException("Plaćanje ne može biti vezano i za narudžbu i za rezervaciju.");

        var korisnikExists = await _context.Korisniks.AnyAsync(x => x.KorisnikId == request.KorisnikId);
        if (!korisnikExists)
            throw new ClientException("Korisnik ne postoji.");

        var nacinExists = await _context.NacinPlacanjas.AnyAsync(x => x.NacinPlacanjaId == request.NacinPlacanjaId);
        if (!nacinExists)
            throw new ClientException("Način plaćanja ne postoji.");

        var pendingStatus = await _context.StatusPlacanjas.FirstOrDefaultAsync(x => x.Naziv == "Pending");
        if (pendingStatus == null)
            throw new ClientException("Status plaćanja 'Pending' ne postoji u bazi.");

        decimal iznos;

        if (request.NarudzbaId.HasValue)
        {
            var narudzba = await _context.Narudzbas.FindAsync(request.NarudzbaId.Value);
            if (narudzba == null)
                throw new ClientException("Narudžba ne postoji.");

            if (narudzba.KorisnikId != request.KorisnikId)
                throw new ClientException("Narudžba ne pripada korisniku.");

            var existsPayment = await _context.Placanjes.AnyAsync(x => x.NarudzbaId == request.NarudzbaId);
            if (existsPayment)
                throw new ClientException("Za ovu narudžbu već postoji plaćanje.");

            iznos = narudzba.UkupnaCijena;
        }
        else
        {
            var rezervacija = await _context.Rezervacijas.FindAsync(request.RezervacijaId!.Value);
            if (rezervacija == null)
                throw new ClientException("Rezervacija ne postoji.");

            if (rezervacija.KorisnikId != request.KorisnikId)
                throw new ClientException("Rezervacija ne pripada korisniku.");

            var existsPayment = await _context.Placanjes.AnyAsync(x => x.RezervacijaId == request.RezervacijaId);
            if (existsPayment)
                throw new ClientException("Za ovu rezervaciju već postoji plaćanje.");

            iznos = rezervacija.UkupnaCijena;
        }

        if (iznos <= 0)
            throw new ClientException("Iznos plaćanja mora biti veći od 0.");

        var accessToken = await GetAccessTokenAsync();

        var currency = _configuration["PayPal:Currency"] ?? "USD";

        var body = new
        {
            intent = "CAPTURE",
            purchase_units = new[]
            {
                new
                {
                    amount = new
                    {
                        currency_code = currency,
                        value = iznos.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)
                    }
                }
            },
            application_context = new
            {
                return_url = _configuration["PayPal:ReturnUrl"],
                cancel_url = _configuration["PayPal:CancelUrl"]
            }
        };

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/v2/checkout/orders");
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        requestMessage.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(requestMessage);
        var json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ClientException($"PayPal create order greška: {json}");

        using var document = JsonDocument.Parse(json);

        var payPalOrderId = document.RootElement.GetProperty("id").GetString()!;

        var approvalUrl = document.RootElement
            .GetProperty("links")
            .EnumerateArray()
            .First(x => x.GetProperty("rel").GetString() == "approve")
            .GetProperty("href")
            .GetString()!;

        var placanje = new Placanje
        {
            KorisnikId = request.KorisnikId,
            NarudzbaId = request.NarudzbaId,
            RezervacijaId = request.RezervacijaId,
            NacinPlacanjaId = request.NacinPlacanjaId,
            StatusPlacanjaId = pendingStatus.StatusPlacanjaId,
            Iznos = iznos,
            TransakcijaId = payPalOrderId,
            DatumKreiranja = DateTime.UtcNow,
            DatumPlacanja = null
        };

        _context.Placanjes.Add(placanje);
        await _context.SaveChangesAsync();

        return new PayPalCreateOrderResponse
        {
            PlacanjeId = placanje.PlacanjeId,
            PayPalOrderId = payPalOrderId,
            ApprovalUrl = approvalUrl
        };
    }

    public async Task<PlacanjeResponse> CaptureOrderAsync(PayPalCaptureOrderRequest request)
    {
        var placanje = await _context.Placanjes
            .Include(x => x.Korisnik)
            .Include(x => x.StatusPlacanja)
            .Include(x => x.NacinPlacanja)
            .FirstOrDefaultAsync(x => x.TransakcijaId == request.PayPalOrderId);

        if (placanje == null)
            throw new ClientException("Plaćanje nije pronađeno.");

        if (placanje.StatusPlacanja.Naziv == "Completed")
            return _mapper.Map<PlacanjeResponse>(placanje);

        var completedStatus = await _context.StatusPlacanjas.FirstOrDefaultAsync(x => x.Naziv == "Completed");
        if (completedStatus == null)
            throw new ClientException("Status plaćanja 'Completed' ne postoji u bazi.");

        var accessToken = await GetAccessTokenAsync();

        var requestMessage = new HttpRequestMessage(
            HttpMethod.Post,
            $"/v2/checkout/orders/{request.PayPalOrderId}/capture");

        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        requestMessage.Content = new StringContent("{}", Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(requestMessage);
        var json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ClientException($"PayPal capture greška: {json}");

        var captureId = ExtractCaptureId(json);

        placanje.StatusPlacanjaId = completedStatus.StatusPlacanjaId;
        placanje.DatumPlacanja = DateTime.UtcNow;
        placanje.TransakcijaId = captureId ?? request.PayPalOrderId;

        await _context.SaveChangesAsync();

        return _mapper.Map<PlacanjeResponse>(placanje);
    }

    public async Task<PovratRefundResponse> RefundAsync(PovratRefundInsertRequest request)
    {
        var placanje = await _context.Placanjes
            .Include(x => x.StatusPlacanja)
            .FirstOrDefaultAsync(x => x.PlacanjeId == request.PlacanjeId);

        if (placanje == null)
            throw new ClientException("Plaćanje ne postoji.");

        if (placanje.StatusPlacanja.Naziv != "Completed")
            throw new ClientException("Refund je moguć samo za uspješno plaćanje.");

        if (request.Iznos <= 0 || request.Iznos > placanje.Iznos)
            throw new ClientException("Neispravan iznos refund-a.");

        var statusExists = await _context.StatusPovrata.AnyAsync(x => x.StatusPovrataId == request.StatusPovrataId);
        if (!statusExists)
            throw new ClientException("Status povrata ne postoji.");

        var accessToken = await GetAccessTokenAsync();
        var currency = _configuration["PayPal:Currency"] ?? "USD";

        var body = new
        {
            amount = new
            {
                value = request.Iznos.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture),
                currency_code = currency
            },
            note_to_payer = request.Razlog
        };

        var requestMessage = new HttpRequestMessage(
            HttpMethod.Post,
            $"/v2/payments/captures/{placanje.TransakcijaId}/refund");

        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        requestMessage.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(requestMessage);
        var json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ClientException($"PayPal refund greška: {json}");

        using var document = JsonDocument.Parse(json);
        var refundId = document.RootElement.GetProperty("id").GetString();

        var entity = new PovratRefund
        {
            PlacanjeId = request.PlacanjeId,
            StatusPovrataId = request.StatusPovrataId,
            Iznos = request.Iznos,
            Razlog = request.Razlog,
            RefundTransakcijaId = refundId,
            DatumZahtjeva = DateTime.UtcNow,
            DatumObrade = DateTime.UtcNow
        };

        _context.PovratRefunds.Add(entity);
        await _context.SaveChangesAsync();

        return _mapper.Map<PovratRefundResponse>(entity);
    }

    private async Task<string> GetAccessTokenAsync()
    {
        var clientId = _configuration["PayPal:ClientId"]!;
        var clientSecret = _configuration["PayPal:ClientSecret"]!;

        var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));

        var request = new HttpRequestMessage(HttpMethod.Post, "/v1/oauth2/token");
        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", auth);
        request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

        var response = await _httpClient.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ClientException($"PayPal token greška: {json}");

        using var document = JsonDocument.Parse(json);
        return document.RootElement.GetProperty("access_token").GetString()!;
    }

    private static string? ExtractCaptureId(string json)
    {
        using var document = JsonDocument.Parse(json);

        var purchaseUnits = document.RootElement.GetProperty("purchase_units");

        foreach (var unit in purchaseUnits.EnumerateArray())
        {
            if (!unit.TryGetProperty("payments", out var payments))
                continue;

            if (!payments.TryGetProperty("captures", out var captures))
                continue;

            foreach (var capture in captures.EnumerateArray())
            {
                if (capture.TryGetProperty("id", out var id))
                    return id.GetString();
            }
        }

        return null;
    }
}