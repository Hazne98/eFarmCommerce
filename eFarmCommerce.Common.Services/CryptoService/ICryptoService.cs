namespace eFarmCommerce.Common.Services.Crypto;

public interface ICryptoService
{
    string GenerateSalt();
    string GenerateHash(string password, string salt);
    bool VerifyPassword(string password, string salt, string hash);
}