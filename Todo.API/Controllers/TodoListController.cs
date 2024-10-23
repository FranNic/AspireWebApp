namespace Todo.API.Controllers;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Todo.Application.DTOs;
using Todo.Application.Mappers;
using Todo.Application.TodoLists.Commands;
using Todo.Domain;
using Todo.Infrastructure.Persistence;

[ApiController]
[Route("api/[controller]")]
public class TodoListController : ControllerBase
{
    private readonly ILogger<TodoListController> _logger;
    private readonly IMediator _mediator;
    private readonly TodoDbContext _dbContext;

    public TodoListController(ILogger<TodoListController> logger, TodoDbContext todoContext, IMediator mediator)
    {
        _logger = logger;
        _dbContext = todoContext;
        this._mediator = mediator;
    }

    [HttpGet(Name = "GetAll")]
    public IEnumerable<TodoListDto> Get()
    {
        return _dbContext.TodoLists.AsNoTracking().Include(x => x.Items).Select(x => x.ToDto()).ToArray();
    }

    [HttpGet("{id}", Name = "GetById")]
    public async Task<ActionResult<TodoListDto>> GetById(Guid id)
    {
        TodoList todoList = await _dbContext.TodoLists.AsNoTracking().Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == id);

        if (todoList == null)
        {
            return NotFound();
        }

        return todoList.ToDto();
    }

    [HttpPost]
    public async Task<ActionResult<TodoListDto>> Post(TodoListDto todoListDto)
    {
        CreateTodoListCommand command = new CreateTodoListCommand
        {
            Title = todoListDto.Title
        };

        TodoListDto todoList = await _mediator.Send(command);

        return CreatedAtRoute("GetById", new { id = todoList.Id }, todoList);
    }
}