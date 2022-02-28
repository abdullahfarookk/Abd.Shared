using EventBus.Events;
using Rebus.Handlers;

namespace EventBus;

public interface IEventHandler{}
public interface IEventHandler<in TEvent> : IEventHandler, IHandleMessages<TEvent> where TEvent : IEvent {}