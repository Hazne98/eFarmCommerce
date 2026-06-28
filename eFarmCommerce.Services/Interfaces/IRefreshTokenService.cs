using eFarmCommerce.Model.Access;

namespace eFarmCommerce.Services.Interfaces;

public interface IRefreshTokenService
{
    Task<UserLoginResponse> RefreshAsync(RefreshAccessTokenRequest request);
}