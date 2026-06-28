using System;
using System.Collections.Generic;

namespace eFarmCommerce.Services.Database.Entities;

public partial class Promocija
{
    public int PromocijaId { get; set; }

    public string Naziv { get; set; } = null!;

    public string? Opis { get; set; }

    public decimal PopustProcenat { get; set; }

    public DateTime DatumPocetka { get; set; }

    public DateTime DatumZavrsetka { get; set; }

    public bool Aktivna { get; set; }

    public virtual ICollection<PromocijaOprema> PromocijaOpremas { get; set; } = new List<PromocijaOprema>();

    public virtual ICollection<PromocijaProizvod> PromocijaProizvods { get; set; } = new List<PromocijaProizvod>();
}
