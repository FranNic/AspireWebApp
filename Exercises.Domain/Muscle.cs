namespace Exercises.Domain;

public class Muscle : Entity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<MuscleActivation> Activations { get; set; } = new List<MuscleActivation>();
}
