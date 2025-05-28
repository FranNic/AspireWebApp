namespace Exercises.Infrastructure.Configurations;

public class SetConfiguration : IEntityTypeConfiguration<Set>
{
    public void Configure(EntityTypeBuilder<Set> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Reps).IsRequired();
        builder.Property(s => s.Weight).IsRequired();
        builder.HasOne(s => s.Exercise)
            .WithMany()
            .HasForeignKey(s => s.ExerciseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}