namespace eFarmCommerce.Model.Responses;

public class PromocijaProizvodResponse
{
    public int PromocijaProizvodId { get; set; }
    public int PromocijaId { get; set; }
    public string? PromocijaNaziv { get; set; }
    public int ProizvodId { get; set; }
    public string? ProizvodNaziv { get; set; }
}