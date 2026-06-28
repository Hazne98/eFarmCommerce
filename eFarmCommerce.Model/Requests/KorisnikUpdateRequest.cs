using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class KorisnikUpdateRequest
{
    [Required]
    [StringLength(100)]
    public string Ime { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string Prezime { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string KorisnickoIme { get; set; } = null!;

    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string Email { get; set; } = null!;

    [Phone]
    [StringLength(30)]
    public string? Telefon { get; set; }

    public string? Slika { get; set; }

    [StringLength(255)]
    public string? Adresa { get; set; }

    [Range(1, int.MaxValue)]
    public int GradId { get; set; }

    [Range(1, int.MaxValue)]
    public int UlogaId { get; set; }

    public bool Aktivan { get; set; }
}
