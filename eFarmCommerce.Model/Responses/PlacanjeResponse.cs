using System;

namespace eFarmCommerce.Model.Responses;

public class PlacanjeResponse
{
    public int PlacanjeId { get; set; }
    public int KorisnikId { get; set; }
    public string? KorisnikImePrezime { get; set; }
    public int? NarudzbaId { get; set; }
    public int? RezervacijaId { get; set; }
    public int StatusPlacanjaId { get; set; }
    public string? StatusPlacanjaNaziv { get; set; }
    public int NacinPlacanjaId { get; set; }
    public string? NacinPlacanjaNaziv { get; set; }
    public decimal Iznos { get; set; }
    public string? TransakcijaId { get; set; }
    public DateTime? DatumPlacanja { get; set; }
    public DateTime DatumKreiranja { get; set; }
}