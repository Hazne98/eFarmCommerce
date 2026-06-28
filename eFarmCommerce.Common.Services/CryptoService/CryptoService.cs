using System.Security.Cryptography;
using System.Text;

namespace eFarmCommerce.Common.Services.Crypto;

public class CryptoService : ICryptoService
{
    public string GenerateSalt()
    {
        var bytes = RandomNumberGenerator.GetBytes(16);
        return Convert.ToBase64String(bytes);
    }

    public string GenerateHash(string password, string salt)
    {
        using var sha256 = SHA256.Create();

        var combined = $"{password}{salt}";
        var bytes = Encoding.UTF8.GetBytes(combined);
        var hash = sha256.ComputeHash(bytes);

        return Convert.ToBase64String(hash);
    }

    public bool VerifyPassword(string password, string salt, string hash)
    {
        var calculatedHash = GenerateHash(password, salt);
        return calculatedHash == hash;
    }
}