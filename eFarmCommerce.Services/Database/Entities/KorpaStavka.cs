using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class KorpaStavka
{
    public int KorpaStavkaId { get; set; }

    public int KorpaId { get; set; }

    public int ProizvodId { get; set; }

    public int Kolicina { get; set; }

    public DateTime DatumDodavanja { get; set; }

    public virtual Korpa Korpa { get; set; } = null!;

    public virtual Proizvod Proizvod { get; set; } = null!;
}
