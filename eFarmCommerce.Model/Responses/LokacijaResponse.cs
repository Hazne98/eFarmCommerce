namespace eFarmCommerce.Model.Responses;

public class LokacijaResponse
{
    public int LokacijaId { get; set; }

    public string Naziv { get; set; } = null!;

    public string Adresa { get; set; } = null!;

    public string? Slika { get; set; }

    public int GradId { get; set; }

    public string? GradNaziv { get; set; }

    public bool Aktivan { get; set; }
}