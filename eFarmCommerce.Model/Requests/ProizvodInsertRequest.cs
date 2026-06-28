namespace eFarmCommerce.Model.Requests;

public class ProizvodInsertRequest
{
    public string Naziv { get; set; } = null!;
    public string? Opis { get; set; }
    public decimal Cijena { get; set; }
    public int KolicinaNaStanju { get; set; }
    public string? Slika { get; set; }
    public int KategorijaProizvodaId { get; set; }
    public int ProizvodjacId { get; set; }
    public bool Aktivan { get; set; } = true;
}