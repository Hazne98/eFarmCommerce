using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class ObavijestInsertRequest
{
    [Required]
    [StringLength(150)]
    public string Naslov { get; set; } = null!;

    [Required]
    [StringLength(2000)]
    public string Tekst { get; set; } = null!;

    public string? Slika { get; set; }

    public bool Aktivna { get; set; } = true;

    public int? KreiraoKorisnikId { get; set; }
}
