using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class Lokacija
{
    public int LokacijaId { get; set; }

    public string Naziv { get; set; } = null!;

    public string Adresa { get; set; } = null!;

    public string? Slika { get; set; }

    public int GradId { get; set; }

    public bool Aktivan { get; set; }

    public virtual Grad Grad { get; set; } = null!;

    public virtual ICollection<OpremaLokacija> OpremaLokacijas { get; set; } = new List<OpremaLokacija>();
}
