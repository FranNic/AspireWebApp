namespace Exercises.Infrastructure;

using Exercises.Infrastructure.Persistence;

using Exercises.Application.Common.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

public static class ProjectAssembly
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ExerciseDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("localhost"),
                    b => b.MigrationsAssembly(typeof(ExerciseDbContext).Assembly.FullName)));

        services.AddScoped<IExercisesDbContext>(provider => provider.GetRequiredService<ExerciseDbContext>());

        return services;
    }

    public static readonly Assembly Assembly = typeof(ProjectAssembly).Assembly;
}