using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class Notifikacija
{
    public int NotifikacijaId { get; set; }

    public int KorisnikId { get; set; }

    public string Naslov { get; set; } = null!;

    public string Tekst { get; set; } = null!;

    public bool Procitana { get; set; }

    public DateTime DatumKreiranja { get; set; }

    public virtual Korisnik Korisnik { get; set; } = null!;
}
