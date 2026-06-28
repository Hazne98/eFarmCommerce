using System;

namespace eFarmCommerce.Model.Responses;

public class PromocijaResponse
{
    public int PromocijaId { get; set; }
    public string Naziv { get; set; } = null!;
    public string? Opis { get; set; }
    public decimal PopustProcenat { get; set; }
    public DateTime DatumPocetka { get; set; }
    public DateTime DatumZavrsetka { get; set; }
    public bool Aktivna { get; set; }
}