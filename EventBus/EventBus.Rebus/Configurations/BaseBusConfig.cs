using Rebus.Config;

namespace EventBus.Rebus.Configurations;

public abstract class BaseBusConfig
{
    public abstract BaseSubscriptionConfig SubscriptionConfig { get; set; }
    public string ConnectionString { get; protected set; }
    public string MessageQueue { get; set; } = "InQueue";
    public string ErrorQueue { get; set; } = "ErrorQueue";
    public bool UseSerilog { get; set; } = true;
    public abstract void Configure(RebusConfigurer configurer);
}