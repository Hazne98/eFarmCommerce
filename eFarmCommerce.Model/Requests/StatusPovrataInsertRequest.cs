using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class StatusPovrataInsertRequest
{
    [Required(ErrorMessage = "Naziv statusa povrata je obavezan.")]
    [StringLength(50)]
    public string Naziv { get; set; } = null!;
}
