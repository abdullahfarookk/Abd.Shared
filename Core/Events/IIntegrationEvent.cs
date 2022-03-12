namespace Abd.Shared.Core.Events;

public interface IIntegrationEvent : IEvent { }

public interface ITopicEvent : IIntegrationEvent
{
    string Topic { get; }
}