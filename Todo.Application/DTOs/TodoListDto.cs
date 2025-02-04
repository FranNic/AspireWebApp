namespace Todo.Application.DTOs;

using System;
using System.Collections.Generic;

public class TodoListDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<TodoItemDto> Items { get; set; } = new();
    public Guid Version { get; set; }
}