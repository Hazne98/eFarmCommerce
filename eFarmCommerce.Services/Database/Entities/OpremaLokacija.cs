using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class OpremaLokacija
{
    public int OpremaLokacijaId { get; set; }

    public int OpremaId { get; set; }

    public int LokacijaId { get; set; }

    public int KolicinaDostupna { get; set; }

    public virtual Lokacija Lokacija { get; set; } = null!;

    public virtual Oprema Oprema { get; set; } = null!;

    public virtual ICollection<RezervacijaStavka> RezervacijaStavkas { get; set; } = new List<RezervacijaStavka>();
}
