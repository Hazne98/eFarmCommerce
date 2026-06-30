using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class PlacanjeUpdateRequest
{
    [Range(1, int.MaxValue)]
    public int StatusPlacanjaId { get; set; }

    [StringLength(200)]
    public string? TransakcijaId { get; set; }

    public DateTime? DatumPlacanja { get; set; }
}