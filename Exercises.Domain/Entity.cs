namespace Exercises.Domain;

using System.ComponentModel.DataAnnotations;

public abstract class Entity
{
    [ConcurrencyCheck]
    public Guid Version { get; set; }
}