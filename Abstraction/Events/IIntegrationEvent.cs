namespace Abd.Shared.Abstraction.Events;

public interface IIntegrationEvent : IEvent { }

public interface ITopicEvent : IIntegrationEvent
{
    string Topic { get; }
}