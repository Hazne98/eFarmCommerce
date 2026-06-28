using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class PromocijaOpremaUpdateRequest
{
    [Range(1, int.MaxValue)]
    public int PromocijaId { get; set; }

    [Range(1, int.MaxValue)]
    public int OpremaId { get; set; }
}
