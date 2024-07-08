namespace Todo.Application;

using Microsoft.EntityFrameworkCore;

using Todo.Domain;

public interface ITodoDbContext
{
    DbSet<TodoItem> TodoItems { get; }
}