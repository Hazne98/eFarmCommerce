using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class PromocijaInsertRequest
{
    [Required]
    [StringLength(150)]
    public string Naziv { get; set; } = null!;

    [StringLength(1000)]
    public string? Opis { get; set; }

    [Range(0, 100)]
    public decimal PopustProcenat { get; set; }

    [Required]
    public DateTime DatumPocetka { get; set; }

    [Required]
    public DateTime DatumZavrsetka { get; set; }

    public bool Aktivna { get; set; } = true;
}
