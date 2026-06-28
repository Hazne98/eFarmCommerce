using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class PlacanjeInsertRequest
{
    [Range(1, int.MaxValue)]
    public int KorisnikId { get; set; }

    public int? NarudzbaId { get; set; }

    public int? RezervacijaId { get; set; }

    [Range(1, int.MaxValue)]
    public int StatusPlacanjaId { get; set; }

    [Range(1, int.MaxValue)]
    public int NacinPlacanjaId { get; set; }

    [Range(0.01, 1000000)]
    public decimal Iznos { get; set; }

    [StringLength(200)]
    public string? TransakcijaId { get; set; }
}
