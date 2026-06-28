using System.ComponentModel.DataAnnotations;

namespace eFarmCommerce.Model.Requests;

public class OpremaUpdateRequest
{
    [Required(ErrorMessage = "Naziv opreme je obavezan.")]
    [StringLength(150, ErrorMessage = "Naziv opreme može imati maksimalno 150 karaktera.")]
    public string Naziv { get; set; } = null!;

    [StringLength(1000, ErrorMessage = "Opis može imati maksimalno 1000 karaktera.")]
    public string? Opis { get; set; }

    [Range(0.01, 100000, ErrorMessage = "Cijena po danu mora biti veća od 0.")]
    public decimal CijenaPoDanu { get; set; }

    public string? Slika { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Kategorija opreme je obavezna.")]
    public int KategorijaOpremeId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Proizvođač je obavezan.")]
    public int ProizvodjacId { get; set; }

    public bool Aktivan { get; set; }
}