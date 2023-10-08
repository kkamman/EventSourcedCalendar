using EventSourcedCalendar.Core.Application.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace EventSourcedCalendar.Core.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        services.AddScoped<ICreateCalendarCommandHandler, CreateCalendarCommandHandler>();
        services.AddScoped<IRenameCalendarCommandHandler, RenameCalendarCommandHandler>();
        return services;
    }
}
