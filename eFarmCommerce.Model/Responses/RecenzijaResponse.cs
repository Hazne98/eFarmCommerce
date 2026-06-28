using System;

namespace eFarmCommerce.Model.Responses;

public class RecenzijaResponse
{
    public int RecenzijaId { get; set; }
    public int KorisnikId { get; set; }
    public string? KorisnikImePrezime { get; set; }
    public int? ProizvodId { get; set; }
    public string? ProizvodNaziv { get; set; }
    public int? OpremaId { get; set; }
    public string? OpremaNaziv { get; set; }
    public int Ocjena { get; set; }
    public string? Komentar { get; set; }
    public DateTime Datum { get; set; }
    public bool Odobrena { get; set; }
}