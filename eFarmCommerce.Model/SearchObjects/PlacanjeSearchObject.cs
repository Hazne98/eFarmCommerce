namespace eFarmCommerce.Model.SearchObjects;

public class PlacanjeSearchObject : BaseSearchObject
{
    public int? KorisnikId { get; set; }
    public int? NarudzbaId { get; set; }
    public int? RezervacijaId { get; set; }
    public int? StatusPlacanjaId { get; set; }
    public int? NacinPlacanjaId { get; set; }
    public DateTime? DatumOd { get; set; }
    public DateTime? DatumDo { get; set; }
    public decimal? IznosOd { get; set; }
    public decimal? IznosDo { get; set; }
}
