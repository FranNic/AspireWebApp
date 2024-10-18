namespace Todo.Application.Common.Interfaces;

using Microsoft.EntityFrameworkCore;

using Todo.Domain;

public interface ITodoDbContext : IDbContext
{
    DbSet<TodoItem> TodoItems { get; }
    DbSet<TodoList> TodoLists { get; }
}