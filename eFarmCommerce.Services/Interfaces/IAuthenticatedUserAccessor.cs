namespace eFarmCommerce.Services.Interfaces;

public interface IAuthenticatedUserAccessor
{
    int KorisnikId { get; }

    string KorisnickoIme { get; }

    string Uloga { get; }
}