namespace Exercises.Application.DTOs;
using System.Collections.Generic;

public class ExerciseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Target { get; set; }
    public int? EquipmentId { get; set; }
    public EquipmentDto? Equipment { get; set; }
    public string? GifUrl { get; set; }

    public List<MuscleActivationDto> MuscleActivations { get; set; } = new List<MuscleActivationDto>();
}