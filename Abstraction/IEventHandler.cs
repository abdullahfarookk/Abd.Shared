using Abd.Shared.Abstraction.Events;

namespace Abd.Shared.Abstraction;
public interface IEventHandler { }

public interface IEventHandler<in TEvent> : IEventHandler where TEvent:IEvent
{
    Task Handle(TEvent @event,CancellationToken cancellationToken = default);
}