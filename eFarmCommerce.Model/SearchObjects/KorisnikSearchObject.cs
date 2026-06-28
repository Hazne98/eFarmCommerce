namespace eFarmCommerce.Model.SearchObjects;

public class KorisnikSearchObject : BaseSearchObject
{
    public string? Ime { get; set; }
    public string? Prezime { get; set; }
    public string? KorisnickoIme { get; set; }
    public string? Email { get; set; }
    public int? GradId { get; set; }
    public int? UlogaId { get; set; }
    public bool? Aktivan { get; set; }
}
