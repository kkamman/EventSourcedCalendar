using EventSourcedCalendar.Core.Domain;

namespace EventSourcedCalendar.Core.Application;

public interface IRepository<T> where T : AggregateRoot
{
    Task Save(
        T aggregate,
        ulong? expectedVersion,
        CancellationToken cancellationToken = default);

    Task<T> GetById(
        Guid id,
        CancellationToken cancellationToken = default);
}
