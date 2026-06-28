using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class LokacijaInsertRequest
{
    [Required(ErrorMessage = "Naziv lokacije je obavezan.")]
    [StringLength(150, ErrorMessage = "Naziv lokacije može imati maksimalno 150 karaktera.")]
    public string Naziv { get; set; } = null!;

    [Required(ErrorMessage = "Adresa je obavezna.")]
    [StringLength(255, ErrorMessage = "Adresa može imati maksimalno 255 karaktera.")]
    public string Adresa { get; set; } = null!;

    public string? Slika { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Grad je obavezan.")]
    public int GradId { get; set; }

    public bool Aktivan { get; set; } = true;
}