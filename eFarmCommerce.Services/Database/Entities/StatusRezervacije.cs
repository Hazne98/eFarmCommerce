using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class StatusRezervacije
{
    public int StatusRezervacijeId { get; set; }

    public string Naziv { get; set; } = null!;

    public virtual ICollection<Rezervacija> Rezervacijas { get; set; } = new List<Rezervacija>();
}
