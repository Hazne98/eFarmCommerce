namespace eFarmCommerce.Model.Requests;

public class PayPalCreateOrderRequest
{
    public int KorisnikId { get; set; }
    public int? NarudzbaId { get; set; }
    public int? RezervacijaId { get; set; }
    public int NacinPlacanjaId { get; set; }
}