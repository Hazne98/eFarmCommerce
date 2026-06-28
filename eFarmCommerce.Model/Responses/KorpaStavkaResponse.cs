using System;

namespace eFarmCommerce.Model.Responses;

public class KorpaStavkaResponse
{
    public int KorpaStavkaId { get; set; }
    public int KorpaId { get; set; }
    public int ProizvodId { get; set; }
    public string? ProizvodNaziv { get; set; }
    public int Kolicina { get; set; }
    public decimal? ProizvodCijena { get; set; }
    public decimal Ukupno => (ProizvodCijena ?? 0) * Kolicina;
    public DateTime DatumDodavanja { get; set; }
}