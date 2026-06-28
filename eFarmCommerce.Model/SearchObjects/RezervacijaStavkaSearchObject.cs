namespace eFarmCommerce.Model.SearchObjects;

public class RezervacijaStavkaSearchObject : BaseSearchObject
{
    public int? RezervacijaId { get; set; }
    public int? OpremaLokacijaId { get; set; }
    public int? OpremaId { get; set; }
    public int? LokacijaId { get; set; }
    public string? OpremaNaziv { get; set; }
    public string? LokacijaNaziv { get; set; }
}
