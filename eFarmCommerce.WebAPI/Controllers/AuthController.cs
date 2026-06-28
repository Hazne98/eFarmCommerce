using eFarmCommerce.Model.Access;
using eFarmCommerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eFarmCommerce.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<UserLoginResponse> LoginAsync([FromBody] UserLoginRequest request)
    {
        return await _authService.LoginAsync(request);
    }
}