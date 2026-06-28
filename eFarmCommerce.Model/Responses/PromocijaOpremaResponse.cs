namespace eFarmCommerce.Model.Responses;

public class PromocijaOpremaResponse
{
    public int PromocijaOpremaId { get; set; }
    public int PromocijaId { get; set; }
    public string? PromocijaNaziv { get; set; }
    public int OpremaId { get; set; }
    public string? OpremaNaziv { get; set; }
}