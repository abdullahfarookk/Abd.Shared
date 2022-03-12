using System;
using Abd.Shared.Utils.StringUtils;
using Rebus.Config;
using Rebus.Subscriptions;

namespace EventBus.Rebus.Configurations;

public class SqlServerSubscriptionConfig : BaseSubscriptionConfig
{
    public readonly bool IsCentralized;
    public readonly string ConnectionString;

    public SqlServerSubscriptionConfig(string connectionString, bool isCentralized = true)
    {
        IsCentralized = isCentralized;
        ConnectionString = connectionString;
    }
    public override void Configure(StandardConfigurer<ISubscriptionStorage> subscriptionConfigurer)
    {

        if (ConnectionString.IsNullOrEmpty())
            throw new Exception("SqlServer connection string not defined for Rebus subscriptions");

        subscriptionConfigurer.StoreInSqlServer(ConnectionString, SubscriptionQueue, IsCentralized);
    }
}
public class SqlServerBusConfig : BaseBusConfig
{
    public sealed override BaseSubscriptionConfig SubscriptionConfig { get; set; }
    public SqlServerBusConfig(string connectionString, string queueName, bool isCentralized = true)
    {
        ConnectionString = connectionString;
        MessageQueue = queueName;
        SubscriptionConfig = new SqlServerSubscriptionConfig(connectionString, isCentralized);
    }

    public override void Configure(RebusConfigurer configurer)
    {
        configurer.Transport(t =>
                t.UseSqlServer(new SqlServerTransportOptions(ConnectionString), MessageQueue))
            .Subscriptions(s => SubscriptionConfig.Configure(s));
    }
}