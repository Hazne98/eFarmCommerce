using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class KategorijaProizvodaUpdateRequest
{
    [Required(ErrorMessage = "Naziv kategorije je obavezan.")]
    [StringLength(100, ErrorMessage = "Naziv kategorije može imati maksimalno 100 karaktera.")]
    public string Naziv { get; set; } = null!;
}