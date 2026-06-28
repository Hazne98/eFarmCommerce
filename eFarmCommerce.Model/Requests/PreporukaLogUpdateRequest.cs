using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class PreporukaLogUpdateRequest
{
    public int? ProizvodId { get; set; }

    public int? OpremaId { get; set; }

    [Required]
    [StringLength(500)]
    public string Razlog { get; set; } = null!;

    [Range(0, 100)]
    public decimal Score { get; set; }
}
