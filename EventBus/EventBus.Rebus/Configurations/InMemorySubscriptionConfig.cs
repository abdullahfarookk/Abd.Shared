using Rebus.Config;
using Rebus.Persistence.InMem;
using Rebus.Subscriptions;
using Rebus.Transport.InMem;

namespace EventBus.Rebus.Configurations;

public class InMemorySubscriptionConfig : BaseSubscriptionConfig
{
    public override void Configure(StandardConfigurer<ISubscriptionStorage> subscriptionConfigurer)
    {
        subscriptionConfigurer.StoreInMemory(new InMemorySubscriberStore());
    }
}
public class InMemoryBusBusConfig : BaseBusConfig
{
    public override BaseSubscriptionConfig SubscriptionConfig { get; set; } = new InMemorySubscriptionConfig();

    public override void Configure(RebusConfigurer configurer)
    {
        configurer.Transport(t => t.UseInMemoryTransport(new InMemNetwork(), MessageQueue))
            .Subscriptions(s => SubscriptionConfig.Configure(s));
    }
}