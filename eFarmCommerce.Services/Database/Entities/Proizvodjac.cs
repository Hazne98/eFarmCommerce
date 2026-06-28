using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class Proizvodjac
{
    public int ProizvodjacId { get; set; }

    public string Naziv { get; set; } = null!;

    public string? Email { get; set; }

    public string? Telefon { get; set; }

    public string? Adresa { get; set; }

    public string? Logo { get; set; }

    public int GradId { get; set; }

    public bool Aktivan { get; set; }

    public virtual Grad Grad { get; set; } = null!;

    public virtual ICollection<Oprema> Opremas { get; set; } = new List<Oprema>();

    public virtual ICollection<Proizvod> Proizvods { get; set; } = new List<Proizvod>();
}
