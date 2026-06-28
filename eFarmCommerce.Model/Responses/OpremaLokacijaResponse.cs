namespace eFarmCommerce.Model.Responses;

public class OpremaLokacijaResponse
{
    public int OpremaLokacijaId { get; set; }

    public int OpremaId { get; set; }

    public string? OpremaNaziv { get; set; }

    public int LokacijaId { get; set; }

    public string? LokacijaNaziv { get; set; }

    public string? LokacijaAdresa { get; set; }

    public int KolicinaDostupna { get; set; }
}