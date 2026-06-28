using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class PovratRefund
{
    public int PovratRefundId { get; set; }

    public int PlacanjeId { get; set; }

    public int StatusPovrataId { get; set; }

    public decimal Iznos { get; set; }

    public string Razlog { get; set; } = null!;

    public string? RefundTransakcijaId { get; set; }

    public DateTime DatumZahtjeva { get; set; }

    public DateTime? DatumObrade { get; set; }

    public virtual Placanje Placanje { get; set; } = null!;

    public virtual StatusPovratum StatusPovrata { get; set; } = null!;
}
