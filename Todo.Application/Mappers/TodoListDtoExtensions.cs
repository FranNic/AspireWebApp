namespace Todo.Application.Mappers;
using System.Linq;

using Todo.Application.DTOs;
using Todo.Domain;

public static class TodoListDtoExtensions
{
    public static TodoListDto ToDto(this TodoList list)
    {
        return new TodoListDto
        {
            Id = list.Id,
            Title = list.Title,
            Items = list.Items.Select(i => i.ToDto()).ToList(),
            Version = list.Version
        };
    }

    public static TodoList ToEntity(this TodoListDto list)
    {
        return new TodoList
        {
            Id = list.Id,
            Title = list.Title,
            Items = list.Items.Select(i => i.ToEntity()).ToList(),
            Version = list.Version
        };
    }
}