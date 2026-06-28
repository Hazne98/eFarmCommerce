using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class PlacanjeUpdateRequest
{
    [Range(1, int.MaxValue)]
    public int StatusPlacanjaId { get; set; }

    [Range(1, int.MaxValue)]
    public int NacinPlacanjaId { get; set; }

    [Range(0.01, 1000000)]
    public decimal Iznos { get; set; }

    [StringLength(200)]
    public string? TransakcijaId { get; set; }

    public DateTime? DatumPlacanja { get; set; }
}
