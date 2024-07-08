namespace Todo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TodoList
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<TodoItem> Items { get; set; } = new List<TodoItem>();
}
