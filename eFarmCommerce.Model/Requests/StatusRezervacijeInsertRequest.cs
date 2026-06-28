using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class StatusRezervacijeInsertRequest
{
    [Required(ErrorMessage = "Naziv statusa rezervacije je obavezan.")]
    [StringLength(50)]
    public string Naziv { get; set; } = null!;
}
