using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class StatusNarudzbeUpdateRequest
{
    [Required(ErrorMessage = "Naziv statusa narudžbe je obavezan.")]
    [StringLength(50, ErrorMessage = "Naziv statusa narudžbe može imati maksimalno 50 karaktera.")]
    public string Naziv { get; set; } = null!;
}