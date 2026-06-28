using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class NarudzbaUpdateRequest
{
    [Range(1, int.MaxValue)]
    public int StatusNarudzbeId { get; set; }

    [Range(0, 1000000)]
    public decimal UkupnaCijena { get; set; }

    [Required]
    [StringLength(255)]
    public string AdresaDostave { get; set; } = null!;

    public int? OdobrioKorisnikId { get; set; }

    [StringLength(500)]
    public string? RazlogOtkazivanja { get; set; }
}
