namespace eFarmCommerce.Model.SearchObjects;

public class RezervacijaSearchObject : BaseSearchObject
{
    public int? KorisnikId { get; set; }
    public int? StatusRezervacijeId { get; set; }
    public DateTime? DatumPocetkaOd { get; set; }
    public DateTime? DatumPocetkaDo { get; set; }
    public DateTime? DatumZavrsetkaOd { get; set; }
    public DateTime? DatumZavrsetkaDo { get; set; }
    public decimal? CijenaOd { get; set; }
    public decimal? CijenaDo { get; set; }
}
