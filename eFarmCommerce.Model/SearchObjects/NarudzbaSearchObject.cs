using System;

namespace eFarmCommerce.Model.SearchObjects;

public class NarudzbaSearchObject : BaseSearchObject
{
    public int? KorisnikId { get; set; }
    public int? StatusNarudzbeId { get; set; }
    public DateTime? DatumOd { get; set; }
    public DateTime? DatumDo { get; set; }
    public decimal? CijenaOd { get; set; }
    public decimal? CijenaDo { get; set; }
}
