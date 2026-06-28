using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class KategorijaOpremeUpdateRequest
{
    [Required(ErrorMessage = "Naziv kategorije opreme je obavezan.")]
    [StringLength(100, ErrorMessage = "Naziv kategorije opreme može imati maksimalno 100 karaktera.")]
    public string Naziv { get; set; } = null!;
}