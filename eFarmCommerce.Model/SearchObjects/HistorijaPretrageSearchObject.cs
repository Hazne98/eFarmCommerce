namespace eFarmCommerce.Model.SearchObjects;

public class HistorijaPretrageSearchObject : BaseSearchObject
{
    public int? KorisnikId { get; set; }
    public string? Pojam { get; set; }
    public string? Tip { get; set; }
    public DateTime? DatumOd { get; set; }
    public DateTime? DatumDo { get; set; }
}
