using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class HistorijaPretrage
{
    public int HistorijaPretrageId { get; set; }

    public int KorisnikId { get; set; }

    public string Pojam { get; set; } = null!;

    public string Tip { get; set; } = null!;

    public DateTime DatumPretrage { get; set; }

    public virtual Korisnik Korisnik { get; set; } = null!;
}
