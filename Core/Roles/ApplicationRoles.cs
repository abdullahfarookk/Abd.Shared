

// ReSharper disable InconsistentNaming
namespace Abd.Shared.Core.Roles;

public static class ApplicationRoles
{
    public const string OAuthAdmin = "Administrator";
    public const string Admin = "admin";

    public static IEnumerable<string?> GetApplicationRoles()
        => typeof(ApplicationRoles)
            .GetFields(BindingFlags.Static | BindingFlags.Public).SelectMany(appOrRole =>
                appOrRole.FieldType == typeof(string)
                    ? new[] { appOrRole.GetValue(null)?.ToString() }
                    : appOrRole.FieldType
                        .GetFields()
                        .Select(field => field.GetValue(appOrRole.GetValue(null))?.ToString()))
            .Distinct();
}