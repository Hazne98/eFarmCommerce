using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class PromocijaOprema
{
    public int PromocijaOpremaId { get; set; }

    public int PromocijaId { get; set; }

    public int OpremaId { get; set; }

    public virtual Oprema Oprema { get; set; } = null!;

    public virtual Promocija Promocija { get; set; } = null!;
}
