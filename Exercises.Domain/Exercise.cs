namespace Exercises.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Exercise
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public string? Target { get; set; }
    public int? EquipmentId { get; set; }
    public Equipment? Equipment { get; set; }
    public string? GifUrl { get; set; }

    public ICollection<MuscleActivation> MuscleActivations { get; set; } = new List<MuscleActivation>();
}
