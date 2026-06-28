using System;

namespace eFarmCommerce.Model.Responses;

public class ObavijestResponse
{
    public int ObavijestId { get; set; }
    public string Naslov { get; set; } = null!;
    public string Tekst { get; set; } = null!;
    public string? Slika { get; set; }
    public DateTime DatumObjave { get; set; }
    public bool Aktivna { get; set; }
    public int? KreiraoKorisnikId { get; set; }
    public string? KreiraoKorisnikImePrezime { get; set; }
}