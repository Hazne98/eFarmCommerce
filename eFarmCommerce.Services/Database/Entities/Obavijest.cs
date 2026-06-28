using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class Obavijest
{
    public int ObavijestId { get; set; }

    public string Naslov { get; set; } = null!;

    public string Tekst { get; set; } = null!;

    public string? Slika { get; set; }

    public DateTime DatumObjave { get; set; }

    public bool Aktivna { get; set; }

    public int KreiraoKorisnikId { get; set; }

    public virtual Korisnik KreiraoKorisnik { get; set; } = null!;
}
