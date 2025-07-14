namespace Exercises.Application.Mappers;

using Exercises.Application.DTOs;
using Exercises.Domain;

public static class ExerciseExtensions
{
    public static ExerciseDto ToDto(this Exercise exercise)
    {
        return new ExerciseDto
        {
            Id = exercise.Id,
            Name = exercise.Name,
            EquipmentId = exercise.EquipmentId,
            GifUrl = exercise.GifUrl, // Assuming GifUrl is a property in Exercise
            Target = exercise.Target,

            // If Equipment is null, we can set it to null or create a new EquipmentDto with default values
            Equipment = exercise.Equipment != null ? new EquipmentDto
            {
                Id = exercise.Equipment.Id,
                Name = exercise.Equipment.Name // Assuming Equipment has an Id and Name property
            } : null,
            //MuscleActivations = exercise.MuscleActivations.Select(ma => ma.ToDto()).ToList() // Assuming MuscleActivation has a ToDto method
        };
    }

    public static Exercise ToEntity(this ExerciseDto exerciseDto)
    {
        return new Exercise
        {
            Id = exerciseDto.Id,
            Name = exerciseDto.Name,
            EquipmentId = exerciseDto.EquipmentId,
            GifUrl = exerciseDto.GifUrl, // Assuming GifUrl is a property in Exercise
            Target = exerciseDto.Target,
            // If Equipment is null, we can set it to null or create a new Equipment with default values
            Equipment = exerciseDto.Equipment != null ? new Equipment
            {
                Id = exerciseDto.Equipment.Id,
                Name = exerciseDto.Equipment.Name // Assuming Equipment has an Id and Name property
            } : null,
            //MuscleActivations = exerciseDto.MuscleActivations.Select(ma => ma.ToEntity()).ToList() // Assuming MuscleActivation has a ToEntity method
        };
    }
}