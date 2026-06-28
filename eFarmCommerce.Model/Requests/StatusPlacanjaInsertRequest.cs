using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class StatusPlacanjaInsertRequest
{
    [Required(ErrorMessage = "Naziv statusa plaćanja je obavezan.")]
    [StringLength(50)]
    public string Naziv { get; set; } = null!;
}
