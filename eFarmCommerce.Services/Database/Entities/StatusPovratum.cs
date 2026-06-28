using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class StatusPovratum
{
    public int StatusPovrataId { get; set; }

    public string Naziv { get; set; } = null!;

    public virtual ICollection<PovratRefund> PovratRefunds { get; set; } = new List<PovratRefund>();
}
