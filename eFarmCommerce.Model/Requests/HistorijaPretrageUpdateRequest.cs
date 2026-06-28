using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class HistorijaPretrageUpdateRequest
{
    [Required]
    [StringLength(150)]
    public string Pojam { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string Tip { get; set; } = null!;
}
