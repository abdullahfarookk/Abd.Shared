using Abd.Shared.Core.Events;

namespace Abd.Shared.Core.EventHandlers;
public interface IEventHandler { }

public interface IEventHandler<TEvent> : IEventHandler
{
    Task Handle(TEvent @event);
}
public interface IEventHandlerr<TEvent> : IEventHandler<TEvent> where TEvent:IEvent
{
}