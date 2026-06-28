using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class KorisnikPasswordChangeRequest
{
    [Required]
    public string StaraLozinka { get; set; } = null!;

    [Required]
    [MinLength(6)]
    public string NovaLozinka { get; set; } = null!;

    [Required]
    [Compare(nameof(NovaLozinka))]
    public string PotvrdaNoveLozinke { get; set; } = null!;
}
