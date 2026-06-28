namespace eFarmCommerce.Model.SearchObjects;

public class PromocijaProizvodSearchObject : BaseSearchObject
{
    public int? PromocijaId { get; set; }
    public int? ProizvodId { get; set; }
    public string? ProizvodNaziv { get; set; }
}
