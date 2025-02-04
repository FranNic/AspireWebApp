namespace Todo.Application.TodoLists.Commands;

using EventBus.Events;

using MassTransit;
using MassTransit.SqlTransport.Topology;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Todo.Application.Common.Interfaces;
using Todo.Application.DTOs;
using Todo.Application.Mappers;
using Todo.Domain;

public class CreateTodoListCommand : IRequest<TodoListDto>
{
    public string Title { get; set; }

    public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, TodoListDto>
    {
        private readonly ITodoDbContext _context;
        private readonly IBus _bus;

        public CreateTodoListCommandHandler(ITodoDbContext context, IBus bus)
        {
            this._context = context;
            this._bus = bus;
        }

        public async Task<TodoListDto> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
        {
            TodoList entity = new TodoList
            {
                Title = request.Title,
            };

            await _context.TodoLists.AddAsync(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _ = _bus.Publish(new IntegrationEvent
            {
                Id = entity.Id,
                Message = "Todo list Created",
            });

            return entity.ToDto();
        }
    }
}
