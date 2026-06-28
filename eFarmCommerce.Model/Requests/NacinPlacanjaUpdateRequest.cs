using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class NacinPlacanjaUpdateRequest
{
    [Required(ErrorMessage = "Naziv načina plaćanja je obavezan.")]
    [StringLength(50)]
    public string Naziv { get; set; } = null!;
}
