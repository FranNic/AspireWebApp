namespace Todo.Application.DTOs;

using System;

[Serializable]
public class TodoItemDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public DateTime? DueDate { get; set; }
    public Guid ListId { get; set; }
    public Guid Version { get; set; }
}