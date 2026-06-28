namespace eFarmCommerce.Model.SearchObjects;

public class PreporukaLogSearchObject : BaseSearchObject
{
    public int? KorisnikId { get; set; }
    public int? ProizvodId { get; set; }
    public int? OpremaId { get; set; }
    public DateTime? DatumOd { get; set; }
    public DateTime? DatumDo { get; set; }
}
