namespace Todo.Application.TodoItems.Commands;

using EventBus.Events;

using MassTransit;

using MediatR;

using System;
using System.Threading.Tasks;

using Todo.Application.Common.Interfaces;
using Todo.Application.DTOs;
using Todo.Application.Mappers;
using Todo.Domain;

public class CreateTodoItemCommand : IRequest<TodoItemDto>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid ListId { get; set; }

    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, TodoItemDto>
    {
        private readonly ITodoDbContext _context;
        private readonly IBus _bus;

        public CreateTodoItemCommandHandler(ITodoDbContext context, IBus bus)
        {
            _context = context;
            _bus = bus;
        }

        public async Task<TodoItemDto> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return null;
            }

            TodoItem entity = new TodoItem
            {
                Title = request.Title,
                Description = request.Description,
                ListId = request.ListId
            };

            await _context.TodoItems.AddAsync(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _ = _bus.Publish(new IntegrationEvent
            {
                Id = entity.Id,
                Message = "TodoItem Created",
            });

            return entity.ToDto();
        }
    }
}