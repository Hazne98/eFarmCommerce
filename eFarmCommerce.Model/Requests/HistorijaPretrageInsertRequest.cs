using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class HistorijaPretrageInsertRequest
{
    [Range(1, int.MaxValue)]
    public int KorisnikId { get; set; }

    [Required]
    [StringLength(150)]
    public string Pojam { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string Tip { get; set; } = null!;
}
