using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace eFarmCommerce.WebAPI.Filters;

public class AuthorizationAttribute : Attribute, IAuthorizationFilter
{
    private readonly string[] _roles;

    public AuthorizationAttribute(params string[] roles)
    {
        _roles = roles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (user?.Identity == null || !user.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedObjectResult(new
            {
                message = "Niste autentifikovani."
            });

            return;
        }

        if (_roles.Length == 0)
            return;

        var userRole = user.FindFirstValue(ClaimTypes.Role);

        if (string.IsNullOrWhiteSpace(userRole) || !_roles.Contains(userRole))
        {
            context.Result = new ForbidResult();
        }
    }
}