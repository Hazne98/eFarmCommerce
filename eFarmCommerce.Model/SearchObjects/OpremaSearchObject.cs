namespace eFarmCommerce.Model.SearchObjects;

public class OpremaSearchObject : BaseSearchObject
{
    public string? Naziv { get; set; }

    public int? KategorijaOpremeId { get; set; }

    public int? ProizvodjacId { get; set; }

    public decimal? CijenaOd { get; set; }

    public decimal? CijenaDo { get; set; }

    public bool? Aktivan { get; set; }

    public bool IncludeKategorija { get; set; } = true;

    public bool IncludeProizvodjac { get; set; } = true;
}