using EventSourcedCalendar.Core.Domain;
using EventStore.Client;

namespace EventSourcedCalendar.Core.Application;

public interface IEventStore
{
    Task StoreEventsForAggregate(
        Guid aggregateId,
        string streamName,
        IEnumerable<IEvent> events,
        StreamRevision expectedVersion,
        CancellationToken cancellationToken = default);

    Task<List<IEvent>> GetEventsForAggregate(
        Guid aggregateId,
        string streamName,
        CancellationToken cancellationToken = default);
}
