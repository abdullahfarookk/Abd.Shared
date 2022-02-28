
namespace Abd.Shared.Core.Authentication;

public static class TokenValidator
{
    public static TokenValidationParameters SkipValidation(string validIssuer, string? validAudience = null, string? secret = null)
    {
        var result = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidIssuer = validIssuer,
            ValidateAudience = false,
            ValidAudience = validAudience.IsNullOrEmpty() ? validIssuer : validAudience,
            ValidateIssuerSigningKey = false,
            //comment this and add this line to fool the validation logic
            SignatureValidator = delegate (string token, TokenValidationParameters _)
            {
                var jwt = new JwtSecurityToken(token);

                return jwt;
            },
            RequireExpirationTime = false,
            ValidateLifetime = false,
            ClockSkew = TimeSpan.Zero,
            RequireSignedTokens = false,
        };

        if (!secret.IsNullOrEmpty())
            result.IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret!));

        return result;
    }
}