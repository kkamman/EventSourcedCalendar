using EventSourcedCalendar.Core.Application;
using EventSourcedCalendar.Core.Domain;
using EventStore.Client;
using System.Text.Json;

namespace EventSourcedCalendar.Infrastructure.EventStore;

internal class EventStore : IEventStore
{
    private readonly EventStoreClient _client;
    private readonly EventTypeNameMap _eventTypeNameMap;

    public EventStore(
        EventStoreClient client,
        EventTypeNameMap eventTypeNameMap)
    {
        _client = client;
        _eventTypeNameMap = eventTypeNameMap;
    }

    public async Task StoreEventsForAggregate(
        Guid aggregateId,
        string streamName,
        IEnumerable<IEvent> events,
        StreamRevision expectedVersion,
        CancellationToken cancellationToken = default)
    {
        var eventData = events.Select(e => new EventData(
            Uuid.NewUuid(),
            _eventTypeNameMap.GetNameForType(e),
            JsonSerializer.SerializeToUtf8Bytes(e, e.GetType())));

        await _client.AppendToStreamAsync(
            string.Join("__", streamName, aggregateId),
            expectedVersion,
            eventData,
            cancellationToken: cancellationToken);
    }

    public async Task<List<IEvent>> GetEventsForAggregate(
        Guid aggregateId,
        string streamName,
        CancellationToken cancellationToken = default)
    {
        var resolvedEvents = await _client
            .ReadStreamAsync(
                Direction.Forwards,
                string.Join("__", streamName, aggregateId),
                StreamPosition.Start,
                cancellationToken: cancellationToken)
            .ToListAsync(cancellationToken);

        return resolvedEvents
            .Select(x => JsonSerializer.Deserialize(
                x.Event.Data.ToArray(),
                _eventTypeNameMap.GetTypeForName(x.Event.EventType)))
            .Cast<IEvent>()
            .ToList();
    }
}
