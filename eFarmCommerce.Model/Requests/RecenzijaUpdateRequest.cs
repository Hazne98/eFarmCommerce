using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class RecenzijaUpdateRequest
{
    [Range(1, 5)]
    public int Ocjena { get; set; }

    [StringLength(1000)]
    public string? Komentar { get; set; }

    public bool Odobrena { get; set; }
}
