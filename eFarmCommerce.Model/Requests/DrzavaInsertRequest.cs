using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class DrzavaInsertRequest
{
    [Required(ErrorMessage = "Naziv države je obavezan.")]
    [StringLength(100, ErrorMessage = "Naziv države može imati maksimalno 100 karaktera.")]
    public string Naziv { get; set; } = null!;
}