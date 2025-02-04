using MassTransit;

using MassTransitDemo;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransit(x =>
{
    //    x.AddConsumer<PingConsumer>();
    //    x.AddConsumer<CounterIncrementedConsumer>();
    x.AddConsumer<HelloWorldMessageConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        var configuration = context.GetRequiredService<IConfiguration>();
        var host = configuration.GetConnectionString("RabbitMQConnection");
        cfg.Host(host);
        cfg.ConfigureEndpoints(context);
    });
});

//builder.Services.AddHostedService<PingPublisher>();


var host = builder.Build();
host.Run();

