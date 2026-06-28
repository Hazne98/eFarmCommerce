using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class StatusNarudzbe
{
    public int StatusNarudzbeId { get; set; }

    public string Naziv { get; set; } = null!;

    public virtual ICollection<Narudzba> Narudzbas { get; set; } = new List<Narudzba>();
}
