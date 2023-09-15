using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Auth.Service.Application.Base;

public abstract class AuthenticatedHandler
{
    protected readonly IHttpContextAccessor HttpContextAccessor;

    protected AuthenticatedHandler(IHttpContextAccessor httpContextAccessor)
    {
        HttpContextAccessor = httpContextAccessor;
    }

    protected Guid UserId
    {
        get
        {
            var userIdClaim = HttpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim is null)
                throw new MissingClaimException(ClaimTypes.Name);

            if (!Guid.TryParse(userIdClaim, out var userIdGuid))
                throw new FormatException("UserId is not a valid GUID.");

            return userIdGuid;
        }
    }

    protected string UserName
    {
        get
        {
            var userId = HttpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value;

            if (userId is null)
                throw new MissingClaimException(ClaimTypes.Name);

            return userId;
        }
    }

    protected string UserEmail
    {
        get
        {
            var userEmail = HttpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            if (userEmail is null)
                throw new MissingClaimException(ClaimTypes.Email);

            return userEmail;
        }
    }
}
