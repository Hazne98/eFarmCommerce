using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class SimpleInsertRequest
{
    [Required(ErrorMessage = "Naziv je obavezan.")]
    [StringLength(100, ErrorMessage = "Naziv može imati maksimalno 100 karaktera.")]
    public string Naziv { get; set; } = null!;
}