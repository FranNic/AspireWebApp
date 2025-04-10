namespace Exercise.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Exercises.Domain;

public class ExerciseDbContext : DbContext, IExerciseDbContext
{
    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<BodyPart> BodyParts => Set<BodyPart>();
    public DbSet<Equipment> Equipment => Set<Equipment>();
    public DbSet<MuscleActivation> MuscleActivations => Set<MuscleActivation>();
    public DbSet<Muscle> Muscles => Set<Muscle>();
    public DbSet<Set> Sets => Set<Set>();

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

        modelBuilder.Entity<MuscleActivation>()
            .HasKey(ma => new { ma.ExerciseId, ma.MuscleId });

        modelBuilder.Entity<MuscleActivation>()
            .HasOne(ma => ma.Exercise)
            .WithMany(e => e.MuscleActivations)
            .HasForeignKey(ma => ma.ExerciseId);

        modelBuilder.Entity<MuscleActivation>()
            .HasOne(ma => ma.Muscle)
            .WithMany(m => m.Activations)
            .HasForeignKey(ma => ma.MuscleId);

        modelBuilder.Entity<MuscleActivation>()
            .HasOne<ActivationLevel>()
            .WithMany()
            .HasForeignKey(ma => ma.ActivationLevelId);
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