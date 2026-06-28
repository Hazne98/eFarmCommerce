namespace eFarmCommerce.Model.SearchObjects;

public class NotifikacijaSearchObject : BaseSearchObject
{
    public int? KorisnikId { get; set; }
    public bool? Procitana { get; set; }
    public DateTime? DatumOd { get; set; }
    public DateTime? DatumDo { get; set; }
}
