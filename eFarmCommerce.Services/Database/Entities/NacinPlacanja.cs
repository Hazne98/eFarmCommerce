using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class NacinPlacanja
{
    public int NacinPlacanjaId { get; set; }

    public string Naziv { get; set; } = null!;

    public virtual ICollection<Placanje> Placanjes { get; set; } = new List<Placanje>();
}
