namespace Todo.Application.DTOs;

using System;
using System.Collections.Generic;

public class TodoListDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<TodoItemDto> Items { get; set; }
    public Guid Version { get; set; }
}