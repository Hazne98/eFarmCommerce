using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class Proizvod
{
    public int ProizvodId { get; set; }

    public string Naziv { get; set; } = null!;

    public string? Opis { get; set; }

    public decimal Cijena { get; set; }

    public int KolicinaNaStanju { get; set; }

    public string? Slika { get; set; }

    public int KategorijaProizvodaId { get; set; }

    public int ProizvodjacId { get; set; }

    public bool Aktivan { get; set; }

    public virtual ICollection<Favorit> Favorits { get; set; } = new List<Favorit>();

    public virtual KategorijaProizvodum KategorijaProizvoda { get; set; } = null!;

    public virtual ICollection<KorpaStavka> KorpaStavkas { get; set; } = new List<KorpaStavka>();

    public virtual ICollection<NarudzbaStavka> NarudzbaStavkas { get; set; } = new List<NarudzbaStavka>();

    public virtual ICollection<PreporukaLog> PreporukaLogs { get; set; } = new List<PreporukaLog>();

    public virtual Proizvodjac Proizvodjac { get; set; } = null!;

    public virtual ICollection<PromocijaProizvod> PromocijaProizvods { get; set; } = new List<PromocijaProizvod>();

    public virtual ICollection<Recenzija> Recenzijas { get; set; } = new List<Recenzija>();
}
