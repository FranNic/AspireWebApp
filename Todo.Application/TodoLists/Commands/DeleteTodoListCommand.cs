namespace Todo.Application.TodoLists.Commands;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Todo.Application.Common.Interfaces;
using Todo.Domain;

public class DeleteTodoListCommand : IRequest<bool>
{
    public Guid Id { get; set; }

    public class DeleteTodoListCommandHandler : IRequestHandler<DeleteTodoListCommand, bool>
    {
        private readonly ITodoDbContext _context;

        public DeleteTodoListCommandHandler(ITodoDbContext context)
        {
            this._context = context;
        }

        public async Task<bool> Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
        {
            bool exists = await _context.TodoLists.AsNoTracking().AnyAsync(x => x.Id == request.Id);

            if (exists)
            {
                _context.TodoLists.Remove(_context.TodoLists.First(x=>x.Id== request.Id));
                await _context.SaveChangesAsync(cancellationToken);

            }

            return exists;
        }
    }
}

