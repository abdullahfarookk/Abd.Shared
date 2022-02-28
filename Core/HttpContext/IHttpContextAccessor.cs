

namespace Abd.Shared.Core.HttpContext;

public static class HttpContextAccessorUtils
{
    public static OAuthSession OAuthSession(this IHttpContextAccessor httpContext) => new OAuthSession(httpContext.HttpContext.User);
    public static string? GetUserId(this IHttpContextAccessor httpContext)
        => httpContext.HttpContext?.User?.FindFirst(JwtClaimTypes.Subject)?.Value;
    public static string? TenantId(this IHttpContextAccessor httpContext)
        => httpContext.HttpContext?.User?.FindFirst(ApplicationClaims.TenantId)?.Value;
    public static string? TenantName(this IHttpContextAccessor httpContext)
        => httpContext.HttpContext?.User?.FindFirst(ApplicationClaims.TenantName)?.Value;
}