using MassTransit;

using MassTransitDemo;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<HelloWorldMessageConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        var configuration = context.GetRequiredService<IConfiguration>();
        var host = configuration.GetConnectionString("RabbitMQConnection");
        cfg.Host(host);
        cfg.ConfigureEndpoints(context);
    });
});

var host = builder.Build();
host.Run();

