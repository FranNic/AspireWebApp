namespace ToDo.Domain;

using MediatR;

public record class TodoItemCompletedEvent(TodoItem TodoItem) : INotification
{
}