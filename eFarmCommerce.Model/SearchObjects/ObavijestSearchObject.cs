namespace eFarmCommerce.Model.SearchObjects;

public class ObavijestSearchObject : BaseSearchObject
{
    public string? Naslov { get; set; }
    public bool? Aktivna { get; set; }
    public int? KreiraoKorisnikId { get; set; }
    public DateTime? DatumOd { get; set; }
    public DateTime? DatumDo { get; set; }
}
