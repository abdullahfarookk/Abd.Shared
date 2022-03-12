using Abd.Shared.Core.Claims;
using Microsoft.AspNetCore.Http;

namespace Abd.Shared.Infrastructure.HttpContextAccessor;

public static class HttpContextAccessorUtils
{
    public static Abd.Shared.Core.Session.Session OAuthSession(this IHttpContextAccessor httpContext) => new Abd.Shared.Core.Session.Session(httpContext.HttpContext.User);
    public static string? GetUserId(this IHttpContextAccessor httpContext)
        => httpContext.HttpContext?.User?.FindFirst(ApplicationClaims.UserId)?.Value;
    public static string? TenantId(this IHttpContextAccessor httpContext)
        => httpContext.HttpContext?.User?.FindFirst(ApplicationClaims.TenantId)?.Value;
    public static string? TenantName(this IHttpContextAccessor httpContext)
        => httpContext.HttpContext?.User?.FindFirst(ApplicationClaims.TenantName)?.Value;
}