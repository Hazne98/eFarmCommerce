using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class KategorijaProizvodum
{
    public int KategorijaProizvodaId { get; set; }

    public string Naziv { get; set; } = null!;

    public virtual ICollection<Proizvod> Proizvods { get; set; } = new List<Proizvod>();
}
