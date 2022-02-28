using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventBus.Events;

namespace EventBus;

public interface IEventBus
{
    Task Publish(IEnumerable<IEvent> events, CancellationToken cancellationToken = default);
    Task Publish(IEvent @event, CancellationToken cancellationToken = default);
}