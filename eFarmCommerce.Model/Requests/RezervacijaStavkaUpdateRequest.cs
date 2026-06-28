using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class RezervacijaStavkaUpdateRequest
{
    [Range(1, int.MaxValue)]
    public int RezervacijaId { get; set; }

    [Range(1, int.MaxValue)]
    public int OpremaLokacijaId { get; set; }

    [Range(1, int.MaxValue)]
    public int Kolicina { get; set; }

    [Range(0.01, 1000000)]
    public decimal CijenaPoDanu { get; set; }
}
