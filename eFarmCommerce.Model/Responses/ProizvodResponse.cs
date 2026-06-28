namespace eFarmCommerce.Model.Responses;

public class ProizvodResponse
{
    public int ProizvodId { get; set; }
    public string Naziv { get; set; } = null!;
    public string? Opis { get; set; }
    public decimal Cijena { get; set; }
    public int KolicinaNaStanju { get; set; }
    public string? Slika { get; set; }
    public int KategorijaProizvodaId { get; set; }
    public int ProizvodjacId { get; set; }
    public bool Aktivan { get; set; }
}