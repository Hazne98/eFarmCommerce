using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class PovratRefundUpdateRequest
{
    [Range(1, int.MaxValue)]
    public int StatusPovrataId { get; set; }

    [Range(0.01, 1000000)]
    public decimal Iznos { get; set; }

    [Required]
    [StringLength(500)]
    public string Razlog { get; set; } = null!;

    [StringLength(200)]
    public string? RefundTransakcijaId { get; set; }

    public DateTime? DatumObrade { get; set; }
}
