namespace eFarmCommerce.Model.Responses;

public class RezervacijaStavkaResponse
{
    public int RezervacijaStavkaId { get; set; }
    public int RezervacijaId { get; set; }
    public int OpremaLokacijaId { get; set; }
    public string? OpremaNaziv { get; set; }
    public string? LokacijaNaziv { get; set; }
    public string? LokacijaAdresa { get; set; }
    public int Kolicina { get; set; }
    public decimal CijenaPoDanu { get; set; }
}