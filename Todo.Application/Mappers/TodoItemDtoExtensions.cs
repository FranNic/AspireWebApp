namespace Todo.Application.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Todo.Application.DTOs;
using Todo.Domain;

public static class TodoItemDtoExtensions
{
    public static TodoItemDto ToDto(this TodoItem item)
    {
        return new TodoItemDto
        {
            Id = item.Id,
            Title = item.Title,
            Description = item.Description,
            IsDone = item.IsDone,
            DueDate = item.CompletedAt,
            Version = item.Version
        };
    }

    public static TodoItem ToEntity(this TodoItemDto item)
    {
        return new TodoItem
        {
            Id = item.Id,
            Title = item.Title,
            Description = item.Description,
            IsDone = item.IsDone,
            CompletedAt = item.DueDate,
            Version = item.Version
        };
    }
}