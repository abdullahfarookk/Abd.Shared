using Abd.Shared.Core.Events;

namespace Abd.Shared.Core.EventBus;
public interface IEventBus
{
    Task Publish(IEnumerable<IEvent> events, CancellationToken cancellationToken = default);
    Task Publish(IEvent @event, CancellationToken cancellationToken = default);
}