namespace Todo.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Todo.Domain;
using Todo.Infrastructure.Persistence;

[ApiController]
[Route("[controller]")]
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
    public IEnumerable<TodoList> Get()
    {
        return _dbContext.TodoLists.Include(x => x.Items).ToArray();


        return Enumerable.Range(1, 5).Select(index => new TodoList
        {
            Id = Guid.NewGuid(),
            Items = new List<TodoItem>
            {
                new TodoItem
                {
                    Id = Guid.NewGuid(),
                    Title = $"Item {index}",
                    IsDone = false
                }
            }
        }).ToArray();
    }
}