using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class RecenzijaInsertRequest
{
    [Range(1, int.MaxValue)]
    public int KorisnikId { get; set; }

    public int? ProizvodId { get; set; }

    public int? OpremaId { get; set; }

    [Range(1, 5)]
    public int Ocjena { get; set; }

    [StringLength(1000)]
    public string? Komentar { get; set; }

    public bool Odobrena { get; set; } = true;
}
