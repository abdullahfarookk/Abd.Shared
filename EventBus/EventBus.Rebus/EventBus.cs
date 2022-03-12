using Abd.Shared.Core.EventBus;
using Abd.Shared.Core.Events;
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
    => @event switch
    {
        null or (IIntegrationEvent and ITopicEvent { Topic: null }) => throw new Exception(
            "No Topic Defined"),
        IIntegrationEvent and ITopicEvent topicEvent => _reBus.Advanced.Topics.Publish(topicEvent.Topic, @event),
        _ => _reBus.Publish(@event)
    };
    public Task Publish(IEnumerable<IEvent> events, CancellationToken cancellationToken = default)
    {
        events = events.ToList();
        if (!events.Any()) return Task.CompletedTask;
        var tasks = events.Select(@event => Publish(@event, cancellationToken));
        return Task.WhenAll(tasks);
    }
}