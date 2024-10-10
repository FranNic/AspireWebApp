namespace AspireWebApp.Web.Extensions;

using AspireWebApp.Web.Components.Pages.Pomodoro;
using AspireWebApp.Web.Components.Pages;
using EventBus.Abstractions;
using MassTransit;
using AspireWebApp.Web.Services.Counter;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddServiceDefaults();
        builder.AddRedisOutputCache("cache");
        builder.Services.AddScoped<PomodoroState>();
        builder.Services.AddSingleton<CounterState>();
        builder.Services.AddScoped<ICounterNotificationService, CounterNotificationService>();

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddHttpClient<WeatherApiClient>(client =>
        {
            // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
            // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
            client.BaseAddress = new("https+http://apiservice");
        });

        builder.Services.AddHttpClient<TodoApiClient>(client =>
        {
            // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
            // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
            client.BaseAddress = new("https+http://todo-api");
        });

        builder.AddMessagingServices();
    }

    private static void AddMessagingServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddMassTransit(x =>
        {
            // using inmemory for demo
            x.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });

            //x.UsingRabbitMq((context, cfg) => { cfg.ConfigureEndpoints(context); });
        });
    }


}
