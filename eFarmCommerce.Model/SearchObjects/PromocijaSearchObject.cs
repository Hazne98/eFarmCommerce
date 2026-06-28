namespace eFarmCommerce.Model.SearchObjects;

public class PromocijaSearchObject : BaseSearchObject
{
    public string? Naziv { get; set; }
    public bool? Aktivna { get; set; }
    public DateTime? DatumPocetkaOd { get; set; }
    public DateTime? DatumPocetkaDo { get; set; }
    public DateTime? DatumZavrsetkaOd { get; set; }
    public DateTime? DatumZavrsetkaDo { get; set; }
}
