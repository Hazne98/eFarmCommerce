using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class RezervacijaStavka
{
    public int RezervacijaStavkaId { get; set; }

    public int RezervacijaId { get; set; }

    public int OpremaLokacijaId { get; set; }

    public int Kolicina { get; set; }

    public decimal CijenaPoDanu { get; set; }

    public virtual OpremaLokacija OpremaLokacija { get; set; } = null!;

    public virtual Rezervacija Rezervacija { get; set; } = null!;
}
