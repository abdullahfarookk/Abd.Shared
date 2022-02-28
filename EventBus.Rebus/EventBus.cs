using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bk.Common.EventBus;
using Bk.Common.EventBus.Events;
using Bk.Common.ObjectUtils;
using Bk.Common.StringUtils;
using Rebus.Bus;

namespace EventBus.Rebus;

public class EventBus : IEventBus
{
    private readonly IBus _reBus;

    public EventBus(IBus reBus)
    {
        _reBus = reBus;
    }

    public Task Publish(IEvent @event, CancellationToken cancellationToken = default)
    {
        if (@event.IsObjNull()) throw new Exception("Event must not be null");

        if (@event is IIntegrationEvent integrationEvent && integrationEvent is ITopicEvent topicEvent)
        {
            if (topicEvent.Topic.IsNullOrEmpty()) throw new Exception("No Topic Defined");
            return _reBus.Advanced.Topics.Publish(topicEvent.Topic, @event);
        }

        return _reBus.Publish(@event);
    }
    public Task Publish(IEnumerable<IEvent> events, CancellationToken cancellationToken = default)
    {
        events = events.ToList();
        if (!events.Any()) return Task.CompletedTask;
        var tasks = events.Select(@event => Publish(@event, cancellationToken));
        return Task.WhenAll(tasks);
    }
}