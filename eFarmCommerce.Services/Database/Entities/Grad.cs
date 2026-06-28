using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class Grad
{
    public int GradId { get; set; }

    public string Naziv { get; set; } = null!;

    public int DrzavaId { get; set; }

    public virtual Drzava Drzava { get; set; } = null!;

    public virtual ICollection<Korisnik> Korisniks { get; set; } = new List<Korisnik>();

    public virtual ICollection<Lokacija> Lokacijas { get; set; } = new List<Lokacija>();

    public virtual ICollection<Proizvodjac> Proizvodjacs { get; set; } = new List<Proizvodjac>();
}
