using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class KategorijaOpreme
{
    public int KategorijaOpremeId { get; set; }

    public string Naziv { get; set; } = null!;

    public virtual ICollection<Oprema> Opremas { get; set; } = new List<Oprema>();
}
