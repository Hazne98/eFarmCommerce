using eFarmCommerce.Common.Services.Crypto;
using eFarmCommerce.Model.Access;
using eFarmCommerce.Model.Exceptions;
using eFarmCommerce.Services.Database;
using eFarmCommerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eFarmCommerce.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly eFarmCommerceDbContext _context;
    private readonly ICryptoService _cryptoService;
    private readonly IConfiguration _configuration;

    public AuthService(
        eFarmCommerceDbContext context,
        ICryptoService cryptoService,
        IConfiguration configuration)
    {
        _context = context;
        _cryptoService = cryptoService;
        _configuration = configuration;
    }

    public async Task<UserLoginResponse> LoginAsync(UserLoginRequest request)
    {
        var korisnik = await _context.Korisniks
            .Include(x => x.Uloga)
            .FirstOrDefaultAsync(x =>
                x.KorisnickoIme == request.KorisnickoIme &&
                x.Aktivan);

        if (korisnik == null)
            throw new ClientException("Pogrešno korisničko ime ili lozinka.");

        var validPassword = _cryptoService.VerifyPassword(
            request.Lozinka,
            korisnik.LozinkaSalt ?? "",
            korisnik.LozinkaHash);

        if (!validPassword)
            throw new ClientException("Pogrešno korisničko ime ili lozinka.");

        var token = GenerateJwtToken(
            korisnik.KorisnikId,
            korisnik.KorisnickoIme,
            korisnik.Uloga.Naziv);

        return new UserLoginResponse
        {
            AccessToken = token,
            RefreshToken = "",
            KorisnikId = korisnik.KorisnikId,
            Ime = korisnik.Ime,
            Prezime = korisnik.Prezime,
            KorisnickoIme = korisnik.KorisnickoIme,
            Uloga = korisnik.Uloga.Naziv
        };
    }

    private string GenerateJwtToken(int korisnikId, string korisnickoIme, string uloga)
    {
        var key = _configuration["Jwt:Key"]!;
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var expiresInMinutes = int.Parse(_configuration["Jwt:ExpiresInMinutes"] ?? "60");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, korisnikId.ToString()),
            new Claim(ClaimTypes.Name, korisnickoIme),
            new Claim(ClaimTypes.Role, uloga)
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(expiresInMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}