using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class Korpa
{
    public int KorpaId { get; set; }

    public int KorisnikId { get; set; }

    public DateTime DatumKreiranja { get; set; }

    public virtual Korisnik Korisnik { get; set; } = null!;

    public virtual ICollection<KorpaStavka> KorpaStavkas { get; set; } = new List<KorpaStavka>();
}
