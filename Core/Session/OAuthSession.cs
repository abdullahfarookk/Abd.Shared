namespace Abd.Shared.Core.Session;

public class OAuthSession
{
    protected readonly ClaimsPrincipal Claims = default!;
    public OAuthSession(ClaimsPrincipal claims)
    {
        Claims = claims;
    }
    public OAuthSession()
    {
    }
    private Guid? _id;

    public Guid Id
    {
        get
        {
            if (_id != null) return _id.Value;
            _id = Guid.Parse(Claims.FindFirst(JwtClaimTypes.Subject)?
                .Value ?? throw new KeyNotFoundException("User does not exist in CustomClaimsTypes"));
            return _id.Value;
        }
        set => _id = value;
    }

    private Guid? _businessId;

    public Guid BusinessId
    {
        get
        {
            if (_businessId != null) return _businessId.Value;
            _businessId = Guid.Parse(Claims.FindFirst(OpenIdClaims.TenantId)?
                .Value ?? throw new KeyNotFoundException("Business Id does not exist in CustomClaimsTypes"));
            return _businessId.Value;
        }
        set => _businessId = value;
    }


    private string? _fullName;

    public string? FullName
    {
        get
        {
            if (_fullName != null) return _fullName;
            _fullName = Claims.FindFirst(JwtClaimTypes.GivenName)?.Value;
            return _fullName;
        }
        set => _fullName = value;
    }

    private string? _email;

    public string? Email
    {
        get
        {
            if (_email != null) return _email;
            _email = Claims.FindFirst(JwtClaimTypes.Email)?.Value;
            return _email;
        }
        set => _email = value;
    }

    private List<string?> _roles = null!;

    public List<string?> Roles
    {
        get
        {
            if (_roles != null) return _roles;
            var roles = Claims.FindAll(JwtClaimTypes.Role);
            _roles = new List<string?>();
            foreach (var role in roles)
            {
                _roles.Add(role?.Value);
            }
            return _roles;
        }
        set => _roles = value;
    }
}