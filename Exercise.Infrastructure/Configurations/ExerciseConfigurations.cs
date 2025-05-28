namespace Exercises.Infrastructure.Configurations;

public class ExcerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Target).HasMaxLength(50);
        builder.Property(e => e.GifUrl).HasMaxLength(200);
        builder.HasMany(e => e.MuscleActivations)
            .WithOne()
            .HasForeignKey(ma => ma.ExerciseId)
            .OnDelete(DeleteBehavior.Cascade);

      
    }
}