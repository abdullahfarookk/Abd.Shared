using Microsoft.Extensions.DependencyInjection;

namespace Abd.Shared.Core.Validation;

public static class ServiceExtension
{
    public static IServiceCollection RegisterValidation(IServiceCollection services,IValidationService validationService)
    {
        services.AddSingleton(validationService);
        return services;
    }
}