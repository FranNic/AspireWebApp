namespace Todo.Application;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

public static class ProjectAssembly
{
    public static readonly Assembly Assembly = typeof(ProjectAssembly).Assembly;

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        return services;
    }
}
