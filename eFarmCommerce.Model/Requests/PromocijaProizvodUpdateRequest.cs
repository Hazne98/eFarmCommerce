using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class PromocijaProizvodUpdateRequest
{
    [Range(1, int.MaxValue)]
    public int PromocijaId { get; set; }

    [Range(1, int.MaxValue)]
    public int ProizvodId { get; set; }
}
