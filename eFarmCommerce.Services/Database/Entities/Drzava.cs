using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class Drzava
{
    public int DrzavaId { get; set; }

    public string Naziv { get; set; } = null!;

    public virtual ICollection<Grad> Grads { get; set; } = new List<Grad>();
}
