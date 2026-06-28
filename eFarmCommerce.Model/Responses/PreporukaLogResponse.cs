using System;

namespace eFarmCommerce.Model.Responses;

public class PreporukaLogResponse
{
    public int PreporukaLogId { get; set; }
    public int KorisnikId { get; set; }
    public string? KorisnikImePrezime { get; set; }
    public int? ProizvodId { get; set; }
    public string? ProizvodNaziv { get; set; }
    public int? OpremaId { get; set; }
    public string? OpremaNaziv { get; set; }
    public string Razlog { get; set; } = null!;
    public decimal Score { get; set; }
    public DateTime DatumPreporuke { get; set; }
}