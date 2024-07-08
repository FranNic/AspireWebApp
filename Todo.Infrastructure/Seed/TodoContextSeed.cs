namespace Todo.Infrastructure.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                        CreatedAt = DateTime.UtcNow
                    },
                    new TodoItem
                    {
                        Title = "Milk",
                        Description = "Buy 2l of milk",
                        CreatedAt = DateTime.UtcNow
                    },
                    new TodoItem
                    {
                        Title = "Bread",
                        Description = "Buy a loaf of bread",
                        CreatedAt = DateTime.UtcNow
                    },
                    new TodoItem
                    {
                        Title = "Toilet paper",
                        Description = "Buy a pack of toilet paper",
                        CreatedAt = DateTime.UtcNow
                    }
                }
            };

            context.TodoLists.Add(todoList);
            await context.SaveChangesAsync();
        }
    }
}
