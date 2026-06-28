using System;

namespace eFarmCommerce.Model.Responses;

public class KorpaResponse
{
    public int KorpaId { get; set; }
    public int KorisnikId { get; set; }
    public string? KorisnikImePrezime { get; set; }
    public DateTime DatumKreiranja { get; set; }
}