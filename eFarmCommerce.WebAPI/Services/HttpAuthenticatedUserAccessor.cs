using eFarmCommerce.Services.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace eFarmCommerce.WebAPI.Services;

public class HttpAuthenticatedUserAccessor : IAuthenticatedUserAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpAuthenticatedUserAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int KorisnikId
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?.User
                .FindFirstValue(ClaimTypes.NameIdentifier);

            return int.TryParse(value, out var id) ? id : 0;
        }
    }

    public string KorisnickoIme
    {
        get
        {
            return _httpContextAccessor.HttpContext?.User
                .FindFirstValue(ClaimTypes.Name) ?? string.Empty;
        }
    }

    public string Uloga
    {
        get
        {
            return _httpContextAccessor.HttpContext?.User
                .FindFirstValue(ClaimTypes.Role) ?? string.Empty;
        }
    }
}