namespace EventBus.Abstractions;

using Microsoft.Extensions.DependencyInjection;

public interface IEventBusBuilder
{
    public IServiceCollection Services { get; }
}