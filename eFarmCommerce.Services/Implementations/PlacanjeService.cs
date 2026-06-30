using eFarmCommerce.Model.Exceptions;
using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Database;
using eFarmCommerce.Services.Database.Entities;
using eFarmCommerce.Services.Interfaces;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace eFarmCommerce.Services.Implementations;

public class PlacanjeService
    : BaseCRUDService<PlacanjeResponse, PlacanjeSearchObject, Placanje, PlacanjeInsertRequest, PlacanjeUpdateRequest>,
      IPlacanjeService
{
    public PlacanjeService(eFarmCommerceDbContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    protected override IQueryable<Placanje> AddFilter(IQueryable<Placanje> query, PlacanjeSearchObject search)
    {
        query = query
            .Include(x => x.Korisnik)
            .Include(x => x.StatusPlacanja)
            .Include(x => x.NacinPlacanja)
            .Include(x => x.Narudzba)
            .Include(x => x.Rezervacija);

        if (search.KorisnikId.HasValue)
            query = query.Where(x => x.KorisnikId == search.KorisnikId.Value);

        if (search.NarudzbaId.HasValue)
            query = query.Where(x => x.NarudzbaId == search.NarudzbaId.Value);

        if (search.RezervacijaId.HasValue)
            query = query.Where(x => x.RezervacijaId == search.RezervacijaId.Value);

        if (search.StatusPlacanjaId.HasValue)
            query = query.Where(x => x.StatusPlacanjaId == search.StatusPlacanjaId.Value);

        if (search.NacinPlacanjaId.HasValue)
            query = query.Where(x => x.NacinPlacanjaId == search.NacinPlacanjaId.Value);

        if (search.DatumOd.HasValue)
            query = query.Where(x => x.DatumKreiranja >= search.DatumOd.Value);

        if (search.DatumDo.HasValue)
            query = query.Where(x => x.DatumKreiranja <= search.DatumDo.Value);

        if (search.IznosOd.HasValue)
            query = query.Where(x => x.Iznos >= search.IznosOd.Value);

        if (search.IznosDo.HasValue)
            query = query.Where(x => x.Iznos <= search.IznosDo.Value);

        return query;
    }

    public override async Task<PlacanjeResponse> InsertAsync(PlacanjeInsertRequest request)
    {
        if (request.NarudzbaId == null && request.RezervacijaId == null)
            throw new ClientException("Plaćanje mora biti vezano za narudžbu ili rezervaciju.");

        if (request.NarudzbaId != null && request.RezervacijaId != null)
            throw new ClientException("Plaćanje ne može biti vezano i za narudžbu i za rezervaciju.");

        var korisnikExists = await Context.Korisniks
            .AnyAsync(x => x.KorisnikId == request.KorisnikId);

        if (!korisnikExists)
            throw new ClientException("Korisnik ne postoji.");

        var nacinPlacanjaExists = await Context.NacinPlacanjas
            .AnyAsync(x => x.NacinPlacanjaId == request.NacinPlacanjaId);

        if (!nacinPlacanjaExists)
            throw new ClientException("Način plaćanja ne postoji.");

        var pendingStatus = await Context.StatusPlacanjas
            .FirstOrDefaultAsync(x => x.Naziv == "Pending");

        if (pendingStatus == null)
            throw new ClientException("Status plaćanja 'Pending' ne postoji u bazi.");

        decimal iznos;

        if (request.NarudzbaId.HasValue)
        {
            var narudzba = await Context.Narudzbas
                .FirstOrDefaultAsync(x => x.NarudzbaId == request.NarudzbaId.Value);

            if (narudzba == null)
                throw new ClientException("Narudžba ne postoji.");

            if (narudzba.KorisnikId != request.KorisnikId)
                throw new ClientException("Narudžba ne pripada odabranom korisniku.");

            var alreadyPaid = await Context.Placanjes
                .AnyAsync(x => x.NarudzbaId == request.NarudzbaId.Value);

            if (alreadyPaid)
                throw new ClientException("Za ovu narudžbu već postoji plaćanje.");

            iznos = narudzba.UkupnaCijena;
        }
        else
        {
            var rezervacija = await Context.Rezervacijas
                .FirstOrDefaultAsync(x => x.RezervacijaId == request.RezervacijaId!.Value);

            if (rezervacija == null)
                throw new ClientException("Rezervacija ne postoji.");

            if (rezervacija.KorisnikId != request.KorisnikId)
                throw new ClientException("Rezervacija ne pripada odabranom korisniku.");

            var alreadyPaid = await Context.Placanjes
                .AnyAsync(x => x.RezervacijaId == request.RezervacijaId.Value);

            if (alreadyPaid)
                throw new ClientException("Za ovu rezervaciju već postoji plaćanje.");

            iznos = rezervacija.UkupnaCijena;
        }

        if (iznos <= 0)
            throw new ClientException("Iznos plaćanja mora biti veći od 0.");

        var entity = new Placanje
        {
            KorisnikId = request.KorisnikId,
            NarudzbaId = request.NarudzbaId,
            RezervacijaId = request.RezervacijaId,
            NacinPlacanjaId = request.NacinPlacanjaId,
            StatusPlacanjaId = pendingStatus.StatusPlacanjaId,
            Iznos = iznos,
            TransakcijaId = request.TransakcijaId,
            DatumKreiranja = DateTime.UtcNow,
            DatumPlacanja = null
        };

        Context.Placanjes.Add(entity);
        await Context.SaveChangesAsync();

        return Mapper.Map<PlacanjeResponse>(entity);
    }

    public override async Task<PlacanjeResponse?> UpdateAsync(int id, PlacanjeUpdateRequest request)
    {
        var entity = await Context.Placanjes.FindAsync(id);

        if (entity == null)
            return default;

        var statusExists = await Context.StatusPlacanjas
            .AnyAsync(x => x.StatusPlacanjaId == request.StatusPlacanjaId);

        if (!statusExists)
            throw new ClientException("Status plaćanja ne postoji.");

        entity.StatusPlacanjaId = request.StatusPlacanjaId;
        entity.TransakcijaId = request.TransakcijaId;
        entity.DatumPlacanja = request.DatumPlacanja;

        await Context.SaveChangesAsync();

        return Mapper.Map<PlacanjeResponse>(entity);
    }
}