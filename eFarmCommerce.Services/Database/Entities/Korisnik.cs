using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class Korisnik
{
    public int KorisnikId { get; set; }

    public string Ime { get; set; } = null!;

    public string Prezime { get; set; } = null!;

    public string KorisnickoIme { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telefon { get; set; }

    public string LozinkaHash { get; set; } = null!;

    public string? LozinkaSalt { get; set; }

    public string? Slika { get; set; }

    public string? Adresa { get; set; }

    public int GradId { get; set; }

    public int UlogaId { get; set; }

    public bool Aktivan { get; set; }

    public DateTime DatumRegistracije { get; set; }

    public virtual ICollection<Favorit> Favorits { get; set; } = new List<Favorit>();

    public virtual Grad Grad { get; set; } = null!;

    public virtual ICollection<HistorijaPretrage> HistorijaPretrages { get; set; } = new List<HistorijaPretrage>();

    public virtual Korpa? Korpa { get; set; }

    public virtual ICollection<Narudzba> NarudzbaKorisniks { get; set; } = new List<Narudzba>();

    public virtual ICollection<Narudzba> NarudzbaOdobrioKorisniks { get; set; } = new List<Narudzba>();

    public virtual ICollection<Notifikacija> Notifikacijas { get; set; } = new List<Notifikacija>();

    public virtual ICollection<Obavijest> Obavijests { get; set; } = new List<Obavijest>();

    public virtual ICollection<Placanje> Placanjes { get; set; } = new List<Placanje>();

    public virtual ICollection<PreporukaLog> PreporukaLogs { get; set; } = new List<PreporukaLog>();

    public virtual ICollection<Recenzija> Recenzijas { get; set; } = new List<Recenzija>();

    public virtual ICollection<Rezervacija> RezervacijaKorisniks { get; set; } = new List<Rezervacija>();

    public virtual ICollection<Rezervacija> RezervacijaOdobrioKorisniks { get; set; } = new List<Rezervacija>();

    public virtual Uloga Uloga { get; set; } = null!;
}
