using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class KorpaStavkaInsertRequest
{
    [Range(1, int.MaxValue)]
    public int KorpaId { get; set; }

    [Range(1, int.MaxValue)]
    public int ProizvodId { get; set; }

    [Range(1, int.MaxValue)]
    public int Kolicina { get; set; }
}
