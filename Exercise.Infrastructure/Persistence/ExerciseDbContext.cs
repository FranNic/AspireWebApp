namespace Exercises.Infrastructure.Persistence;

using Exercises.Application.Common.Interfaces;
using Exercises.Domain;

using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;

using Microsoft.EntityFrameworkCore;

using System;
using System.Reflection;
using System.Threading.Tasks;

public class ExerciseDbContext : DbContext, IExercisesDbContext
{
    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<Equipment> Equipment => Set<Equipment>();
    public DbSet<MuscleActivation> MuscleActivations => Set<MuscleActivation>();
    public DbSet<Muscle> Muscles => Set<Muscle>();
    public DbSet<Set> Sets => Set<Set>();
    public DbSet<ActivationLevel> ActivationLevels => Set<ActivationLevel>();

    public ExerciseDbContext(DbContextOptions<ExerciseDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.Entity<OutboxState>()
           .HasKey(o => o.OutboxId);

        modelBuilder.Entity<OutboxMessage>()
            .HasKey(o => o.MessageId);

        modelBuilder.Entity<Equipment>()
            .HasKey(e => e.Id);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<Entity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Version = Guid.NewGuid();
                    break;

                case EntityState.Modified:
                    entry.Entity.Version = Guid.NewGuid();
                    break;
            }
        }
        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }
}