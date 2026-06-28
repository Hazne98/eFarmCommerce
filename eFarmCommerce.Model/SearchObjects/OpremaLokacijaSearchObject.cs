namespace eFarmCommerce.Model.SearchObjects;

public class OpremaLokacijaSearchObject : BaseSearchObject
{
    public int? OpremaId { get; set; }

    public int? LokacijaId { get; set; }

    public string? OpremaNaziv { get; set; }

    public string? LokacijaNaziv { get; set; }

    public bool SamoDostupno { get; set; } = false;
}