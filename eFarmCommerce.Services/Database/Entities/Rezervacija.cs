using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class Rezervacija
{
    public int RezervacijaId { get; set; }

    public int KorisnikId { get; set; }

    public int StatusRezervacijeId { get; set; }

    public DateTime DatumPocetka { get; set; }

    public DateTime DatumZavrsetka { get; set; }

    public decimal UkupnaCijena { get; set; }

    public DateTime DatumKreiranja { get; set; }

    public int? OdobrioKorisnikId { get; set; }

    public DateTime? DatumPromjeneStatusa { get; set; }

    public string? RazlogOtkazivanja { get; set; }

    public virtual Korisnik Korisnik { get; set; } = null!;

    public virtual Korisnik? OdobrioKorisnik { get; set; }

    public virtual ICollection<Placanje> Placanjes { get; set; } = new List<Placanje>();

    public virtual ICollection<RezervacijaStavka> RezervacijaStavkas { get; set; } = new List<RezervacijaStavka>();

    public virtual StatusRezervacije StatusRezervacije { get; set; } = null!;
}
