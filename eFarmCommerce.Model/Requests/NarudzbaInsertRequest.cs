using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class NarudzbaInsertRequest
{
    [Range(1, int.MaxValue)]
    public int KorisnikId { get; set; }

    [Range(1, int.MaxValue)]
    public int StatusNarudzbeId { get; set; }

    [Range(0, 1000000)]
    public decimal UkupnaCijena { get; set; }

    [Required]
    [StringLength(255)]
    public string AdresaDostave { get; set; } = null!;
}
