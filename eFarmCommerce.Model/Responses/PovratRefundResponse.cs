using System;

namespace eFarmCommerce.Model.Responses;

public class PovratRefundResponse
{
    public int PovratRefundId { get; set; }
    public int PlacanjeId { get; set; }
    public int StatusPovrataId { get; set; }
    public string? StatusPovrataNaziv { get; set; }
    public decimal Iznos { get; set; }
    public string Razlog { get; set; } = null!;
    public string? RefundTransakcijaId { get; set; }
    public DateTime DatumZahtjeva { get; set; }
    public DateTime? DatumObrade { get; set; }
}