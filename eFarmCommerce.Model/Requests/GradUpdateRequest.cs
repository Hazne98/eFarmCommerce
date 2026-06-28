using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class GradUpdateRequest
{
    [Required(ErrorMessage = "Naziv grada je obavezan.")]
    [StringLength(100, ErrorMessage = "Naziv grada može imati maksimalno 100 karaktera.")]
    public string Naziv { get; set; } = null!;

    [Range(1, int.MaxValue, ErrorMessage = "Država je obavezna.")]
    public int DrzavaId { get; set; }
}