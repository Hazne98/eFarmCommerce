using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class RezervacijaInsertRequest
{
    [Range(1, int.MaxValue)]
    public int KorisnikId { get; set; }

    [Range(1, int.MaxValue)]
    public int StatusRezervacijeId { get; set; }

    [Required]
    public DateTime DatumPocetka { get; set; }

    [Required]
    public DateTime DatumZavrsetka { get; set; }

    [Range(0, 1000000)]
    public decimal UkupnaCijena { get; set; }
}
