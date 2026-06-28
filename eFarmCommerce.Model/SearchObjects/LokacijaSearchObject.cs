namespace eFarmCommerce.Model.SearchObjects;

public class LokacijaSearchObject : BaseSearchObject
{
    public string? Naziv { get; set; }

    public string? Adresa { get; set; }

    public int? GradId { get; set; }

    public bool? Aktivan { get; set; }
}