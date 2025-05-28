namespace Exercises.Infrastructure.Seed;
using Exercises.Domain;
using Exercises.Infrastructure.Persistence;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public static class ExerciseContextSeed
{
    public static async Task SeedExcercisesAsync(this ExerciseDbContext context)
    {
        if (!context.Exercises.Any())
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Seeds/Exercises.json");
            var json = File.ReadAllText(path);
            var exercises = JsonSerializer.Deserialize<ExerciseJsonModel[]>(json);



            var equipmentLookup = context.Equipment.ToDictionary(e => e.Name, e => e.Id);
            // build a foreach with exercises. Lookup the EquipmentId from the equipmentLookup dictionary and set it on each exercise. take batches of 10
            if (exercises?.Length == 0)
                throw new Exception("Failed to deserialize Exercises.json or no exercises found");

            for (int i = 0; i < exercises.Length; i += 10)
            {
                var batch = exercises.Skip(i).Take(10);
                foreach (var exercise in batch)
                {
                    var newExercise = new Exercise
                    {
                        Id = 0,
                        EquipmentName = exercise.EquipmentName,
                        GifUrl = exercise.GifUrl,
                        Name = exercise.Name,
                        Target = exercise.Target
                    };
                    if (!string.IsNullOrEmpty(exercise.EquipmentName) && equipmentLookup.TryGetValue(exercise.EquipmentName, out var equipmentId))
                    {
                        newExercise.EquipmentId = equipmentId;
                    }
                    else
                    {
                        newExercise.EquipmentId = null;
                    }
                context.Exercises.Add(newExercise);
                }
            }


            //var formatted = exercises?.Select(x => 
            //{   
            //    x.Id = 0; 
            //    x.EquipmentId = equipmentLookup[x.EquipmentName]; 
            //    return x; 
            //});
            
            //if (exercises?.Length == 0 || formatted == null)
            //    throw new Exception("Failed to deserialize Exercises.json or no exercises found");
            
            //context.Exercises.AddRange(formatted);
        }
        await context.SaveChangesAsync();
    }

    public static async Task SeedEquipmentAsync(this ExerciseDbContext context)
    {
        if (!context.Equipment.Any())
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Seeds/equipment.json");
            var json = File.ReadAllText(path);
            var equipmentStringList = JsonSerializer.Deserialize<string[]>(json);
            
            if (equipmentStringList?.Length == 0)
                throw new Exception("Failed to deserialize equipment.json");

            var newEquipment = equipmentStringList?.Select(x => new Equipment { Name = x });

            using var transaction = await context.Database.BeginTransactionAsync();
            context.Equipment.AddRange(newEquipment!);
            await transaction.CommitAsync();
        }
        await context.SaveChangesAsync();
    }

    public static async Task SeedMusclesAsync(this ExerciseDbContext context)
    {
        if (!context.Muscles.Any())
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Seeds/Muscles.json");
            var json = File.ReadAllText(path);
            var muscles = JsonSerializer.Deserialize<string[]>(json);

            if (muscles?.Length == 0)
                throw new Exception("Failed to deserialize muscles.json");

            var newMuscles = muscles?.Select(x => new Muscle { Name = x });
            using var transaction = await context.Database.BeginTransactionAsync();
            context.Muscles.AddRange(newMuscles!);
            await transaction.CommitAsync();
        }
        await context.SaveChangesAsync();
    }
}
public class ExerciseJsonModel
{
    [JsonPropertyName("bodyPart")]
    public string BodyPart { get; set; }
    [JsonPropertyName("equipmentName")]
    public string EquipmentName { get; set; }
    [JsonPropertyName("gifUrl")]
    public string GifUrl { get; set; }
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("target")]
    public string Target { get; set; }
}
