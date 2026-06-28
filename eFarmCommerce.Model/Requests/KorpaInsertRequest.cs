using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class KorpaInsertRequest
{
    [Range(1, int.MaxValue)]
    public int KorisnikId { get; set; }
}
