using EventSourcedCalendar.Core.Application;
using EventSourcedCalendar.Core.Domain;
using EventStore.Client;

namespace EventSourcedCalendar.Infrastructure.EventStore;

internal class Repository<T> : IRepository<T> where T : AggregateRoot
{
    private readonly IEventStore _eventStore;
    private readonly StreamNameMap _streamNameMap;

    public Repository(
        IEventStore eventStore,
        StreamNameMap streamNameMap)
    {
        _eventStore = eventStore;
        _streamNameMap = streamNameMap;
    }

    public async Task<T> GetById(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var events = await _eventStore.GetEventsForAggregate(
            id,
            _streamNameMap.GetStreamNameForType<T>(),
            cancellationToken);
        return AggregateRoot.FromStoredEvents<T>(events);
    }

    public async Task Save(
        T aggregate,
        ulong? expectedVersion,
        CancellationToken cancellationToken = default)
    {
        await _eventStore.StoreEventsForAggregate(
            aggregate.Id,
            _streamNameMap.GetStreamNameForType<T>(),
            aggregate.GetUncommittedChanges(),
            expectedVersion ?? StreamRevision.None,
            cancellationToken);
    }
}
