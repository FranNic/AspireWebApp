namespace Todo.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;
using Todo.Application.Common.Interfaces;
using Todo.Infrastructure.Persistence;
using Todo.Infrastructure.Services;

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