using System.Collections.Concurrent;
using Abd.Shared.Core.EventBus;
using Abd.Shared.Core.EventHandlers;
using Abd.Shared.Core.Events;
using Abd.Shared.Utils.ArrayUtils;
using Abd.Shared.Utils.StringUtils;
using Abd.Shared.Utils.TaskUtils;
using EventBus.Rebus.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Retry.Simple;
using Rebus.Routing;
using Rebus.Routing.TypeBased;
using Rebus.Serialization;
using Rebus.Serialization.Json;

namespace EventBus.Rebus;

public static class RebusBusBuilderExtension
{
    private static IEnumerable<Type> _events = new List<Type>();
    private static IEnumerable<string> _subscriptions = new List<string>();
    public static void UseEventBus(this IApplicationBuilder app)
    {
        app.ApplicationServices.StartRebus();
    }
    public static void UseEventBus(this IServiceProvider provider)
    {
        provider.StartRebus();
    }
    private static IEnumerable<Type> GetImplementedHandlerInterfaces(Type type) => type.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventHandler<>));

    public static IServiceCollection AddEventBus(this IServiceCollection services, BaseBusConfig config, Type[]? integrationEvents = null, Action<StandardConfigurer<IRouter>>? routerConfiguration = null)
    {
        _events = !integrationEvents.IsNullOrZero() ? _events.Concat(integrationEvents!) : _events;
        services.ConfigureRebus(config);
        return services;
    }

    public static IServiceCollection AddEventBus(this IServiceCollection services, Type[] allTypes, BaseBusConfig config, Type[]? integrationEvents = null, Action<StandardConfigurer<IRouter>>? routerConfiguration = null)
    {
        _events = !integrationEvents.IsNullOrZero() ? _events.Concat(integrationEvents!) : _events;
        RegisterEventAndHandlers(services, allTypes);
        services.ConfigureRebus(config);
        return services;
    }
    public static IServiceCollection AddEventBus(this IServiceCollection services, Type[] allTypes, BaseBusConfig config, ConcurrentDictionary<string, Type>? subscriptions = null, Action<StandardConfigurer<IRouter>>? routerConfiguration = null)
    {
        RegisterEventAndHandlers(services, allTypes);
        services.ConfigureRebus(config,subscriptions, routerConfiguration);
        return services;
    }
    public static IServiceCollection AddEventBus(this IServiceCollection services, BaseBusConfig config, ConcurrentDictionary<string, Type> subscriptions, Action<StandardConfigurer<IRouter>>? routerConfiguration = null)
    {
        services.ConfigureRebus(config,subscriptions, routerConfiguration);
        return services;
    }
    public static IServiceCollection AddEventBus(this IServiceCollection services, Type[] allTypes, IConfiguration configuration, string? conStr = null, Type[]? integrationEvents = null, Action<StandardConfigurer<IRouter>>? routerConfiguration = null)
    {
        var config = CreateConfig(configuration);
        _events = !integrationEvents.IsNullOrZero() ? _events.Concat(integrationEvents!) : _events;
        services.RegisterEventAndHandlers(allTypes);
        services.ConfigureRebus(config, routerConfig: routerConfiguration);
        return services;
    }
    private static BaseBusConfig CreateConfig(IConfiguration configuration,string? conStr = null)
    {
        var type = configuration["EventBus:Type"];
        var queueName = configuration["EventBus:Name"];

        return type switch
        {
            not string { Length: > 0 } => throw new Exception("No event bus configuration found"),
            "Msmq" => new MsmqBusConfig(),
            "SqlServer" => new SqlServerBusConfig(configuration.GetConnectionString(conStr ?? "SqlServer"), queueName ?? "app-queue"),
            _ => throw new NotImplementedException("EventBus config not implemented properly")
        };
    }
    public static IServiceCollection AddInMemoryEventBus(this IServiceCollection services, Type[] allTypes)
    {
        BaseBusConfig config = new InMemoryBusBusConfig();
        services.RegisterEventAndHandlers(allTypes);
        services.ConfigureRebus(config);
        return services;
    }
    public static bool ParseBool(this IConfiguration configuration, string value)
    {
        var boolStr = configuration[value];

        return !boolStr.IsNullOrEmpty()&&
               bool.TryParse(boolStr, out var parsed) && parsed;
    }
    private static IServiceCollection ConfigureRebus(this 
        IServiceCollection services,
        BaseBusConfig config, 
        ConcurrentDictionary<string, Type>? subscriptions = null,
        Action<StandardConfigurer<IRouter>>? routerConfig = null)
    {
        services.AddScoped<IEventBus, EventBus>();
        services.AddRebus(configure =>
        {
            configure
                .ConfigureConfigurations(config)
                .Routing(routerConfig ?? (x => x.TypeBased()));
            if (!subscriptions.IsNullOrZero())
            {
                RegisterSubscriptions(subscriptions!);
                configure.ConfigureTopicSubscriptions(subscriptions);
            }
            return configure;
        },
        onCreated: SubscribeToEvents());
        return services;
    }
    private static RebusConfigurer ConfigureTopicSubscriptions(this RebusConfigurer configurer,
        ConcurrentDictionary<string, Type>? subscriptions = null)
    {
        return configurer.Serialization(s => s.UseNewtonsoftJson(JsonInteroperabilityMode.PureJson))
            // we extend the default JSON serialization with some extra deserialization help
            .Options(o => o.Decorate<ISerializer>(c => new CustomMessageDeserializer(c.Get<ISerializer>(), subscriptions)));
    }
    private static void RegisterEventAndHandlers(this IServiceCollection services, Type[] types)
    {
        // configure events
        _events = _events.Concat(types
            .Where(type => typeof(IEvent).IsAssignableFrom(type) && !type.IsInterface));

        // configure event handlers
        types
            .Where(type => typeof(IEventHandler).IsAssignableFrom(type) && !type.IsInterface)
            .ForEach(handler =>
                GetImplementedHandlerInterfaces(handler)
                    .ForEach(@event => services.AddTransient(@event, handler)));
    }
    private static void RegisterSubscriptions(ConcurrentDictionary<string, Type> subscriptions)
    {
        if (subscriptions.IsNullOrZero()) return;
        _subscriptions = subscriptions.Select(x => x.Key);
    }
    public static RebusConfigurer ConfigureConfigurations(this RebusConfigurer configurer, BaseBusConfig configuration)
    {
        configurer
            .Options(opt =>
            {
                opt.SimpleRetryStrategy(configuration.ErrorQueue);
                opt.SetDueTimeoutsPollInteval(TimeSpan.FromSeconds(5));
            });

        if (configuration.UseSerilog) configurer.Logging(l => l.Serilog());
        configuration.Configure(configurer);
        return configurer;
    }
    private static Func<IBus, Task> SubscribeToEvents()
    => async bus =>
    {
        await _events.ForEachAsync(@event => bus.Subscribe(@event));
        await _subscriptions.ForEachAsync(subscription => bus.Advanced.Topics.Subscribe(subscription));
    };
}