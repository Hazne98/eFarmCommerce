namespace eFarmCommerce.Model.SearchObjects;

public class ProizvodjacSearchObject : BaseSearchObject
{
    public string? Naziv { get; set; }
    public string? Email { get; set; }
    public int? GradId { get; set; }
    public bool? Aktivan { get; set; }
}