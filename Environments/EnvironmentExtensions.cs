using Microsoft.Extensions.Hosting;

namespace Abd.Shared.Environments;

public static class EnvironmentExtensions
{
    public static bool IsProd(this IHostEnvironment hosting) =>
        hosting?.EnvironmentName == "prod";
    public static bool IsQa(this IHostEnvironment hosting) =>
        hosting?.EnvironmentName == "qa";
    public static bool IsTest(this IHostEnvironment hosting) =>
        hosting?.EnvironmentName == "test";
    public static bool IsDev(this IHostEnvironment hosting) =>
        hosting?.EnvironmentName is "Development" or "dev" or "local";

    public static void Set(this IHostEnvironment hosting, string env)
    {
        hosting.EnvironmentName = env;
        ApplicationEnvironment.SetAppEnvironment(env);
    }
    public static void SetDefault(this IHostEnvironment hosting)
    {
        if (hosting.EnvironmentName is not "" and null) 
            throw new Exception("Environment variable not defined");
        ApplicationEnvironment.SetAppEnvironment(hosting);
    }
}