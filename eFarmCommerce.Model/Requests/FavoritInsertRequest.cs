using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class FavoritInsertRequest
{
    [Range(1, int.MaxValue)]
    public int KorisnikId { get; set; }

    public int? ProizvodId { get; set; }

    public int? OpremaId { get; set; }
}
