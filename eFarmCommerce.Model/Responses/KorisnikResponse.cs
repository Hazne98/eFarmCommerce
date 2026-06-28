using System;

namespace eFarmCommerce.Model.Responses;

public class KorisnikResponse
{
    public int KorisnikId { get; set; }
    public string Ime { get; set; } = null!;
    public string Prezime { get; set; } = null!;
    public string KorisnickoIme { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Telefon { get; set; }
    public string? Slika { get; set; }
    public string? Adresa { get; set; }
    public int GradId { get; set; }
    public string? GradNaziv { get; set; }
    public int UlogaId { get; set; }
    public string? UlogaNaziv { get; set; }
    public bool Aktivan { get; set; }
    public DateTime DatumRegistracije { get; set; }
}