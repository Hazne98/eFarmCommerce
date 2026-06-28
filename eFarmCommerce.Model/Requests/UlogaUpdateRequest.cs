using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class UlogaUpdateRequest
{
    [Required(ErrorMessage = "Naziv uloge je obavezan.")]
    [StringLength(50, ErrorMessage = "Naziv uloge može imati maksimalno 50 karaktera.")]
    public string Naziv { get; set; } = null!;
}