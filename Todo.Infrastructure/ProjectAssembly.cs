namespace Todo.Infrastructure;

using System.Reflection;
using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Infrastructure.Services;
using Todo.Infrastructure.Persistence;
using Todo.Application;

public static class ProjectAssembly
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TodoDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("localhost"),
                    b => b.MigrationsAssembly(typeof(TodoDbContext).Assembly.FullName)));
        
        services.AddTransient<IDateTime, DateTimeService>();

        services.AddScoped<ITodoDbContext>(provider => provider.GetRequiredService<TodoDbContext>());

        return services;
    }

    public static readonly Assembly Assembly = typeof(ProjectAssembly).Assembly;
}
