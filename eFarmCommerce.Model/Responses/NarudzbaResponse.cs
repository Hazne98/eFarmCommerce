using System;

namespace eFarmCommerce.Model.Responses;

public class NarudzbaResponse
{
    public int NarudzbaId { get; set; }
    public int KorisnikId { get; set; }
    public string? KorisnikImePrezime { get; set; }
    public int StatusNarudzbeId { get; set; }
    public string? StatusNarudzbeNaziv { get; set; }
    public DateTime DatumNarudzbe { get; set; }
    public decimal UkupnaCijena { get; set; }
    public string AdresaDostave { get; set; } = null!;
    public int? OdobrioKorisnikId { get; set; }
    public string? OdobrioKorisnikImePrezime { get; set; }
    public DateTime? DatumPromjeneStatusa { get; set; }
    public string? RazlogOtkazivanja { get; set; }
}