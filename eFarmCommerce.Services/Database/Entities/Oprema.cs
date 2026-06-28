using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class Oprema
{
    public int OpremaId { get; set; }

    public string Naziv { get; set; } = null!;

    public string? Opis { get; set; }

    public decimal CijenaPoDanu { get; set; }

    public string? Slika { get; set; }

    public int KategorijaOpremeId { get; set; }

    public int ProizvodjacId { get; set; }

    public bool Aktivan { get; set; }

    public virtual ICollection<Favorit> Favorits { get; set; } = new List<Favorit>();

    public virtual KategorijaOpreme KategorijaOpreme { get; set; } = null!;

    public virtual ICollection<OpremaLokacija> OpremaLokacijas { get; set; } = new List<OpremaLokacija>();

    public virtual ICollection<PreporukaLog> PreporukaLogs { get; set; } = new List<PreporukaLog>();

    public virtual Proizvodjac Proizvodjac { get; set; } = null!;

    public virtual ICollection<PromocijaOprema> PromocijaOpremas { get; set; } = new List<PromocijaOprema>();

    public virtual ICollection<Recenzija> Recenzijas { get; set; } = new List<Recenzija>();
}
