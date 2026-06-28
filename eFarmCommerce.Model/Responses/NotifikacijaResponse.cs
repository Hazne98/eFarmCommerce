using System;

namespace eFarmCommerce.Model.Responses;

public class NotifikacijaResponse
{
    public int NotifikacijaId { get; set; }
    public int KorisnikId { get; set; }
    public string Naslov { get; set; } = null!;
    public string Tekst { get; set; } = null!;
    public bool Procitana { get; set; }
    public DateTime DatumKreiranja { get; set; }
}