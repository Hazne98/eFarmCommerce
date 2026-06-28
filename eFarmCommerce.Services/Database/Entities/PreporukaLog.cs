using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class PreporukaLog
{
    public int PreporukaLogId { get; set; }

    public int KorisnikId { get; set; }

    public int? ProizvodId { get; set; }

    public int? OpremaId { get; set; }

    public string Razlog { get; set; } = null!;

    public decimal Score { get; set; }

    public DateTime DatumPreporuke { get; set; }

    public virtual Korisnik Korisnik { get; set; } = null!;

    public virtual Oprema? Oprema { get; set; }

    public virtual Proizvod? Proizvod { get; set; }
}
