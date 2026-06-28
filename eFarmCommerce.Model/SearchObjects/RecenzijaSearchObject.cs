namespace eFarmCommerce.Model.SearchObjects;

public class RecenzijaSearchObject : BaseSearchObject
{
    public int? KorisnikId { get; set; }
    public int? ProizvodId { get; set; }
    public int? OpremaId { get; set; }
    public int? Ocjena { get; set; }
    public bool? Odobrena { get; set; }
    public DateTime? DatumOd { get; set; }
    public DateTime? DatumDo { get; set; }
}
