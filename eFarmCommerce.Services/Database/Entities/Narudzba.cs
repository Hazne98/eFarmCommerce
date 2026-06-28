using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class Narudzba
{
    public int NarudzbaId { get; set; }

    public int KorisnikId { get; set; }

    public int StatusNarudzbeId { get; set; }

    public DateTime DatumNarudzbe { get; set; }

    public decimal UkupnaCijena { get; set; }

    public string AdresaDostave { get; set; } = null!;

    public int? OdobrioKorisnikId { get; set; }

    public DateTime? DatumPromjeneStatusa { get; set; }

    public string? RazlogOtkazivanja { get; set; }

    public virtual Korisnik Korisnik { get; set; } = null!;

    public virtual ICollection<NarudzbaStavka> NarudzbaStavkas { get; set; } = new List<NarudzbaStavka>();

    public virtual Korisnik? OdobrioKorisnik { get; set; }

    public virtual ICollection<Placanje> Placanjes { get; set; } = new List<Placanje>();

    public virtual StatusNarudzbe StatusNarudzbe { get; set; } = null!;
}
