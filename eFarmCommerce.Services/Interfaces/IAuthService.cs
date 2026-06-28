using eFarmCommerce.Model.Access;

namespace eFarmCommerce.Services.Interfaces;

public interface IAuthService
{
    Task<UserLoginResponse> LoginAsync(UserLoginRequest request);
}