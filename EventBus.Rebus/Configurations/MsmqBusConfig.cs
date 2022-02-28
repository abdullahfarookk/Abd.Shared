using Rebus.Config;

namespace EventBus.Rebus.Configurations;

public class MsmqBusConfig : BaseBusConfig
{
    public sealed override BaseSubscriptionConfig SubscriptionConfig { get; set; }
    public MsmqBusConfig()
    {
        SubscriptionConfig = new InMemorySubscriptionConfig();
    }
    public MsmqBusConfig(string connectionString, bool isCentralized = true)
    {
        SubscriptionConfig = new SqlServerSubscriptionConfig(connectionString, isCentralized);
    }

    public override void Configure(RebusConfigurer configurer)
    {
        configurer.Transport(t => t.UseMsmq(MessageQueue))
            .Subscriptions(s => SubscriptionConfig.Configure(s));
    }
}