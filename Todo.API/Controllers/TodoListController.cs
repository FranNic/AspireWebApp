namespace Todo.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Todo.Application.DTOs;
using Todo.Application.Mappers;
using Todo.Domain;
using Todo.Infrastructure.Persistence;

[ApiController]
[Route("api/[controller]")]
public class TodoListController : ControllerBase
{
    private readonly ILogger<TodoListController> _logger;
    private readonly TodoDbContext _dbContext;

    public TodoListController(ILogger<TodoListController> logger, TodoDbContext todoContext)
    {
        _logger = logger;
        _dbContext = todoContext;
    }

    [HttpGet(Name = "GetAll")]
    public IEnumerable<TodoListDto> Get()
    {
        return _dbContext.TodoLists.AsNoTracking().Include(x => x.Items).Select(x => x.ToDto()).ToArray();
    }

    [HttpGet("{id}", Name = "GetById")]
    public async Task<ActionResult<TodoListDto>> GetById(Guid id)
    {
        var todoList = await _dbContext.TodoLists.AsNoTracking().Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == id);

        if (todoList == null)
        {
            return NotFound();
        }

        return todoList.ToDto();
    }
}