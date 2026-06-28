namespace eFarmCommerce.Model.SearchObjects;

public class ProizvodSearchObject : BaseSearchObject
{
    public string? Naziv { get; set; }
    public int? KategorijaProizvodaId { get; set; }
    public int? ProizvodjacId { get; set; }
    public bool? Aktivan { get; set; }
}