using EventSourcedCalendar.Core.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventSourcedCalendar.Infrastructure.EventStore;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventStore(
        this IServiceCollection services,
        IConfiguration configurationSection)
    {
        services.Configure<EventStoreOptions>(configurationSection);

        var options = new EventStoreOptions();
        configurationSection.Bind(options);

        services.AddEventStoreClient(options.ConnectionString);

        services.AddSingleton<EventTypeNameMap>();
        services.AddSingleton<StreamNameMap>();

        services.AddScoped<IEventStore, EventStore>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}
