using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class NarudzbaStavkaUpdateRequest
{
    [Range(1, int.MaxValue)]
    public int NarudzbaId { get; set; }

    [Range(1, int.MaxValue)]
    public int ProizvodId { get; set; }

    [Range(1, int.MaxValue)]
    public int Kolicina { get; set; }

    [Range(0.01, 1000000)]
    public decimal Cijena { get; set; }
}
