namespace Abd.Shared.Core;

public class Session
{
    private readonly ClaimsPrincipal _claims = default!;

    private static Exception NotFoundError(string val)
        => new KeyNotFoundException($"{val} does not exist in Claims");
    public Session(ClaimsPrincipal claims)
    {
        _claims = claims;
    }
    public Session()
    {
    }
    private Guid? _id;

    public Guid Id
    {
        get
        {
            if (_id != null) return _id.Value;
            _id = Guid.Parse(_claims.FindFirst(ApplicationClaims.UserId)?
                .Value ?? throw NotFoundError("User ID"));
            return _id.Value;
        }
        set => _id = value;
    }

    private Guid? _tenantId;

    public Guid TenantId
    {
        get
        {
            if (_tenantId != null) return _tenantId.Value;
            var tenantClaim = _claims.FindFirst(ApplicationClaims.TenantId) ?? 
                              _claims.FindFirst(OpenIdClaims.TenantId);
            if (tenantClaim is null) throw NotFoundError("Tenant Id");
            _tenantId = Guid.Parse(tenantClaim.Value);
            return _tenantId.Value;
        }
        set => _tenantId = value;
    }
    private string? _fullName;

    public string? FullName
    {
        get
        {
            if (_fullName != null) return _fullName;
            _fullName = _claims.FindFirst(ApplicationClaims.GivenName)?.Value;
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
            _email = _claims.FindFirst(ApplicationClaims.Email)?.Value;
            return _email;
        }
        set => _email = value;
    }

    private List<string?>? _roles;

    public List<string?> Roles
    {
        get
        {
            if (_roles is {}) return _roles;
            var roles = _claims.FindAll(ApplicationClaims.Role);
            _roles ??= new List<string?>();
            foreach (var role in roles)
            {
                _roles.Add(role?.Value);
            }
            return _roles;
        }
        set => _roles = value;
    }
}