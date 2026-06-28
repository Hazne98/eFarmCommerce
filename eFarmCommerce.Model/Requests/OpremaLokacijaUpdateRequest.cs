using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class OpremaLokacijaUpdateRequest
{
    [Range(1, int.MaxValue, ErrorMessage = "Oprema je obavezna.")]
    public int OpremaId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Lokacija je obavezna.")]
    public int LokacijaId { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Količina dostupne opreme ne može biti negativna.")]
    public int KolicinaDostupna { get; set; }
}