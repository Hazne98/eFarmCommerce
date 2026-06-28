namespace eFarmCommerce.Model.Responses;

public class GradResponse
{
    public int GradId { get; set; }

    public string Naziv { get; set; } = null!;

    public int DrzavaId { get; set; }

    public string? DrzavaNaziv { get; set; }
}