namespace eFarmCommerce.Model.SearchObjects;

public class PovratRefundSearchObject : BaseSearchObject
{
    public int? PlacanjeId { get; set; }
    public int? StatusPovrataId { get; set; }
    public DateTime? DatumZahtjevaOd { get; set; }
    public DateTime? DatumZahtjevaDo { get; set; }
    public decimal? IznosOd { get; set; }
    public decimal? IznosDo { get; set; }
}
