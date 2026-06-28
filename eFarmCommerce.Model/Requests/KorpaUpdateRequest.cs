using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class KorpaUpdateRequest
{
    [Range(1, int.MaxValue)]
    public int KorisnikId { get; set; }
}
