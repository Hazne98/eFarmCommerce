using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class NotifikacijaInsertRequest
{
    [Range(1, int.MaxValue)]
    public int KorisnikId { get; set; }

    [Required]
    [StringLength(150)]
    public string Naslov { get; set; } = null!;

    [Required]
    [StringLength(2000)]
    public string Tekst { get; set; } = null!;
}
