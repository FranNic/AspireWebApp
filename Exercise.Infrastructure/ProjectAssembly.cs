namespace Exercise.Infrastructure;

using System.Reflection;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;
using Exercise.Infrastructure.Persistence;

public static class ProjectAssembly
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ExerciseDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("localhost"),
                    b => b.MigrationsAssembly(typeof(TodoDbContext).Assembly.FullName)));

        services.AddTransient<IDateTime, DateTimeService>();

        services.AddScoped<IExerciseDbContext>(provider => provider.GetRequiredService<ExerciseDbContext>());

        return services;
    }

    public static readonly Assembly Assembly = typeof(ProjectAssembly).Assembly;
}