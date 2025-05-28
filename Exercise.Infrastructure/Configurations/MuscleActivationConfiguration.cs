namespace Exercises.Infrastructure.Configurations;
public class MuscleActivationConfiguration : IEntityTypeConfiguration<MuscleActivation>
{
    public void Configure(EntityTypeBuilder<MuscleActivation> builder)
    {
        builder.HasKey(ma => new { ma.ExerciseId, ma.MuscleId });
        
        builder.HasOne(ma => ma.Exercise)
            .WithMany(e => e.MuscleActivations)
            .HasForeignKey(ma => ma.ExerciseId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(ma => ma.Muscle)
            .WithMany()
            .HasForeignKey(ma => ma.MuscleId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(ma => ma.ActivationLevel)
            .WithMany()
            .HasForeignKey(ma => ma.ActivationLevelId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}