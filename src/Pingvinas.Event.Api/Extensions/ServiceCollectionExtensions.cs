using Pingvinas.Event.Core.Common;
using Pingvinas.Event.Core.Features;
using Pingvinas.Event.Core.Features.Notification;
using Pingvinas.Event.Core.Features.PingvinEvent;
using Pingvinas.Event.Domain.Repositories;

namespace Pingvinas.Event.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPingvinasServices(this IServiceCollection services)
        => services.AddTransient<IEventService, EventService>()
            .AddTransient<IEventRepository, EventRepository>()
            .AddTransient<CurrentUser>()
            .AddTransient<INotificationService, CompositeNotificationService>()
            .AddTransient<INotificationChannel, EmailNotificationChannel>()
            .AddTransient<INotificationChannel, SmsNotificationChannel>();
}