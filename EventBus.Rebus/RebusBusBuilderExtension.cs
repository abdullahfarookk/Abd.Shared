using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Bk.Common.ArrayUtils;
using Bk.Common.EventBus;
using Bk.Common.EventBus.Events;
using Bk.Common.StringUtils;
using EventBus.Rebus.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;
using Rebus.Handlers;
using Rebus.Retry.Simple;
using Rebus.Routing;
using Rebus.Routing.TypeBased;
using Rebus.Serialization;
using Rebus.Serialization.Json;
using Rebus.ServiceProvider;

namespace EventBus.Rebus;

public static class RebusBusBuilderExtension
{
    private static IEnumerable<Type> _events = new List<Type>();
    private static IEnumerable<string> _subscriptions = new List<string>();
    public static void UseCustomRebus(this IApplicationBuilder app)
    {
        app.ApplicationServices.UseRebus(bus =>
        {
            _events.ForEach(@event => bus.Subscribe(@event));
            _subscriptions.ForEach(topic => bus.Advanced.Topics.Subscribe(topic));
        });
    }
    private static IEnumerable<Type> GetImplementedHandlerInterfaces(Type type) => type.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandleMessages<>));

    public static IServiceCollection AddRebusEventBus(this IServiceCollection services, BaseBusConfig config, Type[] integrationEvents = null, Action<StandardConfigurer<IRouter>> routerConfiguration = null)
    {
        _events = !integrationEvents.IsNullOrZero() ? _events.Concat(integrationEvents!) : _events;
        services.AddRebus(configure => configure
            .ConfigureConfigurations(config)
            .Routing(routerConfiguration ?? (x => x.TypeBased())));
        return services;
    }
    public static IServiceCollection AddRebusEventBus(this IServiceCollection services, Type[] allTypes, BaseBusConfig config, Type[] integrationEvents = null, Action<StandardConfigurer<IRouter>> routerConfiguration = null)
    {
        _events = !integrationEvents.IsNullOrZero() ? _events.Concat(integrationEvents!) : _events;
        RegisterEventAndHandlers(services, allTypes);
        services.AddRebus(configure => configure
            .ConfigureConfigurations(config)
            .Routing(routerConfiguration ?? (x => x.TypeBased())));
        return services;
    }
    public static IServiceCollection AddRebusEventBus(this IServiceCollection services, Type[] allTypes, BaseBusConfig config, ConcurrentDictionary<string, Type> subscriptions = null, Action<StandardConfigurer<IRouter>> routerConfiguration = null)
    {
        RegisterEventAndHandlers(services, allTypes);

        if (!subscriptions.IsNullOrZero())
            RegisterSubscriptions(subscriptions);

        services.AddRebus(configure => configure
            .ConfigureConfigurations(config)
            // configure serializer to serialize as pure JSONM (i.e. WITHOUT type information inside the serialized format)
            .ConfigureTopicSubscriptions(subscriptions)
            .Routing(routerConfiguration ?? (x => x.TypeBased())));
        return services;
    }
    public static IServiceCollection AddRebusEventBus(this IServiceCollection services, BaseBusConfig config, ConcurrentDictionary<string, Type> subscriptions, Action<StandardConfigurer<IRouter>> routerConfiguration = null)
    {
        RegisterSubscriptions(subscriptions);
        services.AddRebus(configure => configure
            .ConfigureConfigurations(config)
            // configure serializer to serialize as pure JSONM (i.e. WITHOUT type information inside the serialized format)
            .ConfigureTopicSubscriptions(subscriptions)
            .Routing(routerConfiguration ?? (x => x.TypeBased())));
        return services;
    }
    public static IServiceCollection AddRebusEventBus(this IServiceCollection services, Type[] allTypes, IConfiguration configuration, Type[] integrationEvents = null, string inMemorySection = "AppSettings:InMemoryEventBus", string conStr = "QfSqlServer", string queueName = "FinanceQueue", Action<StandardConfigurer<IRouter>> routerConfiguration = null)
    {
        BaseBusConfig config;
        if (configuration.ParseBool(inMemorySection))
            config = new InMemoryBusBusConfig();
        else
            config = new SqlServerBusConfig(configuration.GetConnectionString(conStr), queueName);

        _events = !integrationEvents.IsNullOrZero() ? _events.Concat(integrationEvents!) : _events;
        RegisterEventAndHandlers(services, allTypes);
        services.AddRebus(configure => configure
            .ConfigureConfigurations(config)
            .Routing(routerConfiguration ?? (x => x.TypeBased())));
        return services;
    }
    public static bool ParseBool(this IConfiguration configuration, string value)
    {
        var boolStr = configuration[value];

        return !boolStr.IsNullOrEmpty() &&
               bool.TryParse(boolStr, out var parsed) && parsed;
    }
    private static RebusConfigurer ConfigureTopicSubscriptions(this RebusConfigurer configurer,
        ConcurrentDictionary<string, Type> subscriptions)
    {
        return configurer.Serialization(s => s.UseNewtonsoftJson(JsonInteroperabilityMode.PureJson))
            // we extend the default JSON serialization with some extra deserialization help
            .Options(o => o.Decorate<ISerializer>(c => new CustomMessageDeserializer(c.Get<ISerializer>(), subscriptions)));
    }
    private static void RegisterEventAndHandlers(IServiceCollection services, Type[] types)
    {
        // configure events
        _events = _events.Concat(types
            .Where(type => typeof(IDomainEvent).IsAssignableFrom(type) && !type.IsInterface));

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
}