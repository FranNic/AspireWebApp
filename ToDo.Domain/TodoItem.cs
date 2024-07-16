namespace Todo.Domain;

using System;

public class TodoItem : Entity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; } = string.Empty;
    public bool IsDone { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    public Guid ListId { get; set; }
    public TodoList List { get; set; } = null!;
}