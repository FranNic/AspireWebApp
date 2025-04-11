namespace Exercises.Application.Common.Interfaces;

using Exercises.Domain;

using Microsoft.EntityFrameworkCore;

public interface IExercisesDbContext : IDbContext
{
    DbSet<Exercise> Exercises { get; }
    DbSet<Equipment> Equipment { get; }
    DbSet<MuscleActivation> MuscleActivations { get; }
    DbSet<Muscle> Muscles { get; }
    DbSet<Set> Sets { get; }
    DbSet<ActivationLevel> ActivationLevels { get; }
}