using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class Recenzija
{
    public int RecenzijaId { get; set; }

    public int KorisnikId { get; set; }

    public int? ProizvodId { get; set; }

    public int? OpremaId { get; set; }

    public int Ocjena { get; set; }

    public string? Komentar { get; set; }

    public DateTime Datum { get; set; }

    public bool Odobrena { get; set; }

    public virtual Korisnik Korisnik { get; set; } = null!;

    public virtual Oprema? Oprema { get; set; }

    public virtual Proizvod? Proizvod { get; set; }
}
