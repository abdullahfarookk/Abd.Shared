using Abd.Shared.Abstraction.Events;

namespace Abd.Shared.Abstraction;
public interface IEventBus
{
    Task Publish(IEnumerable<IEvent> events, CancellationToken cancellationToken = default);
    Task Publish(IEvent @event, CancellationToken cancellationToken = default);
}