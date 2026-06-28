using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class ProizvodjacUpdateRequest
{
    [Required]
    [StringLength(100)]
    public string Naziv { get; set; } = null!;

    [EmailAddress]
    [StringLength(254)]
    public string? Email { get; set; }

    [Phone]
    [StringLength(50)]
    public string? Telefon { get; set; }

    [StringLength(200)]
    public string? Adresa { get; set; }

    public string? Logo { get; set; }

    [Required]
    public int GradId { get; set; }

    public bool Aktivan { get; set; }
}