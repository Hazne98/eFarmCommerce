using System;

namespace eFarmCommerce.Model.Responses;

public class FavoritResponse
{
    public int FavoritId { get; set; }
    public int KorisnikId { get; set; }
    public string? KorisnikImePrezime { get; set; }
    public int? ProizvodId { get; set; }
    public string? ProizvodNaziv { get; set; }
    public int? OpremaId { get; set; }
    public string? OpremaNaziv { get; set; }
    public DateTime DatumDodavanja { get; set; }
}