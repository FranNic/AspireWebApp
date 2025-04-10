namespace Exercises.Domain;

public class MuscleActivation
{
    public int ExerciseId { get; set; }
    public Exercise Exercise { get; set; } = null!;

    public int MuscleId { get; set; }
    public Muscle Muscle { get; set; } = null!;

    public int ActivationLevelId { get; set; } // "Primary", etc.
    public ActivationLevel ActivationLevel { get; set; }
}
