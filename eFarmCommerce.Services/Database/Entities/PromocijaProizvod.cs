using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class PromocijaProizvod
{
    public int PromocijaProizvodId { get; set; }

    public int PromocijaId { get; set; }

    public int ProizvodId { get; set; }

    public virtual Proizvod Proizvod { get; set; } = null!;

    public virtual Promocija Promocija { get; set; } = null!;
}
