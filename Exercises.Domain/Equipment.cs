namespace Exercises.Domain;

public class Equipment : Entity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
}