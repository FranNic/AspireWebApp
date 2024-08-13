namespace Todo.Infrastructure.Seed;

using System;
using System.Linq;
using System.Threading.Tasks;

using Todo.Domain;
using Todo.Infrastructure.Persistence;

public static class TodoDbContextSeed
{
    public static async Task SeedSampleDataAsync(TodoDbContext context)
    {
        if (!context.TodoLists.Any())
        {
            var todoList = new TodoList
            {
                Title = "Shopping",
                Items =
                {
                    new TodoItem
                    {
                        Title = "Apples",
                        Description = "Buy a dozen apples",
                        CreatedAt = DateTime.UtcNow,
                        Version = Guid.NewGuid()
                    },
                    new TodoItem
                    {
                        Title = "Milk",
                        Description = "Buy 2l of milk",
                        CreatedAt = DateTime.UtcNow,
                        Version = Guid.NewGuid()
                    },
                    new TodoItem
                    {
                        Title = "Bread",
                        Description = "Buy a loaf of bread",
                        CreatedAt = DateTime.UtcNow,
                        Version = Guid.NewGuid()
                    },
                    new TodoItem
                    {
                        Title = "Toilet paper",
                        Description = "Buy a pack of toilet paper",
                        CreatedAt = DateTime.UtcNow,
                        Version = Guid.NewGuid()
                    }
                },
                Version = Guid.NewGuid()
            };

            context.TodoLists.Add(todoList);
            await context.SaveChangesAsync();
        }
    }
}