using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class Placanje
{
    public int PlacanjeId { get; set; }

    public int KorisnikId { get; set; }

    public int? NarudzbaId { get; set; }

    public int? RezervacijaId { get; set; }

    public int StatusPlacanjaId { get; set; }

    public int NacinPlacanjaId { get; set; }

    public decimal Iznos { get; set; }

    public string? TransakcijaId { get; set; }

    public DateTime? DatumPlacanja { get; set; }

    public DateTime DatumKreiranja { get; set; }

    public virtual Korisnik Korisnik { get; set; } = null!;

    public virtual NacinPlacanja NacinPlacanja { get; set; } = null!;

    public virtual Narudzba? Narudzba { get; set; }

    public virtual ICollection<PovratRefund> PovratRefunds { get; set; } = new List<PovratRefund>();

    public virtual Rezervacija? Rezervacija { get; set; }

    public virtual StatusPlacanja StatusPlacanja { get; set; } = null!;
}
