using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class Favorit
{
    public int FavoritId { get; set; }

    public int KorisnikId { get; set; }

    public int? ProizvodId { get; set; }

    public int? OpremaId { get; set; }

    public DateTime DatumDodavanja { get; set; }

    public virtual Korisnik Korisnik { get; set; } = null!;

    public virtual Oprema? Oprema { get; set; }

    public virtual Proizvod? Proizvod { get; set; }
}
