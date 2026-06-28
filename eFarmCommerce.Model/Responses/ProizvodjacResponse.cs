namespace eFarmCommerce.Model.Responses;

public class ProizvodjacResponse
{
    public int ProizvodjacId { get; set; }
    public string Naziv { get; set; } = null!;
    public string? Email { get; set; }
    public string? Telefon { get; set; }
    public string? Adresa { get; set; }
    public string? Logo { get; set; }
    public int GradId { get; set; }
    public bool Aktivan { get; set; }
}