namespace eFarmCommerce.Model.Responses;

public class OpremaResponse
{
    public int OpremaId { get; set; }

    public string Naziv { get; set; } = null!;

    public string? Opis { get; set; }

    public decimal CijenaPoDanu { get; set; }

    public string? Slika { get; set; }

    public int KategorijaOpremeId { get; set; }

    public string? KategorijaOpremeNaziv { get; set; }

    public int ProizvodjacId { get; set; }

    public string? ProizvodjacNaziv { get; set; }

    public bool Aktivan { get; set; }
}