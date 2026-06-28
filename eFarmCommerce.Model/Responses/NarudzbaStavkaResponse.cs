namespace eFarmCommerce.Model.Responses;

public class NarudzbaStavkaResponse
{
    public int NarudzbaStavkaId { get; set; }
    public int NarudzbaId { get; set; }
    public int ProizvodId { get; set; }
    public string? ProizvodNaziv { get; set; }
    public int Kolicina { get; set; }
    public decimal Cijena { get; set; }
    public decimal Ukupno => Cijena * Kolicina;
}