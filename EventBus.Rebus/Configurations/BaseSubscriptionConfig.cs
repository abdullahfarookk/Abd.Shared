using Rebus.Config;
using Rebus.Subscriptions;

namespace EventBus.Rebus.Configurations;

public abstract class BaseSubscriptionConfig
{
    public string SubscriptionQueue { get; set; } = "TopicQueueSubscriptions";
    public abstract void Configure(StandardConfigurer<ISubscriptionStorage> subscriptionConfigurer);
}