namespace Todo.Domain;

using System;
using System.Collections.Generic;

public class TodoList : Entity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<TodoItem> Items { get; set; } = new List<TodoItem>();
}