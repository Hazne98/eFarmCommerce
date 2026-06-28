using System;

namespace eFarmCommerce.Model.Responses;

public class RezervacijaResponse
{
    public int RezervacijaId { get; set; }
    public int KorisnikId { get; set; }
    public string? KorisnikImePrezime { get; set; }
    public int StatusRezervacijeId { get; set; }
    public string? StatusRezervacijeNaziv { get; set; }
    public DateTime DatumPocetka { get; set; }
    public DateTime DatumZavrsetka { get; set; }
    public decimal UkupnaCijena { get; set; }
    public DateTime DatumKreiranja { get; set; }
    public int? OdobrioKorisnikId { get; set; }
    public string? OdobrioKorisnikImePrezime { get; set; }
    public DateTime? DatumPromjeneStatusa { get; set; }
    public string? RazlogOtkazivanja { get; set; }
}