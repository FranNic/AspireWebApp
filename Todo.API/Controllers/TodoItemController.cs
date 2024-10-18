namespace Todo.API.Controllers;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;

using Todo.Application.DTOs;
using Todo.Application.Mappers;
using Todo.Application.TodoItems.Commands;
using Todo.Domain;
using Todo.Infrastructure.Persistence;

[Route("api/todolist/{todolistId}/[controller]")]
[ApiController]
public class TodoItemController : ControllerBase
{
    private readonly TodoDbContext _dbContext;
    private readonly ILogger<TodoItemController> _logger;
    private readonly IMediator _mediator;

    public TodoItemController(TodoDbContext dbContext, ILogger<TodoItemController> logger, IMediator mediator)
    {
        this._dbContext = dbContext;
        this._logger = logger;
        this._mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodoItem(Guid todolistId, TodoItemDto newItem)
    {
        try
        {
            TodoList? todolist = await _dbContext.TodoLists.AsNoTracking().FirstOrDefaultAsync(x => x.Id == todolistId);
            if (todolist == null)
            {
                return NotFound();
            }

            TodoItemDto item = await _mediator.Send(new CreateTodoItemCommand
            {
                Title = newItem.Title,
                Description = newItem.Description,
                ListId = todolistId
            });

            return Created("GetById", item);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid todolistId, Guid id)
    {
        try
        {
            var todoitem = await _dbContext.TodoItems.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (todoitem == null)
            {
                return NotFound();
            }

            return Ok(todoitem.ToDto());
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    [HttpDelete("{todoId}")]
    public async Task<IActionResult> DeleteTodoItem(Guid todolistId, Guid todoId)
    {
        try
        {
            var todoitemToUpdate = _dbContext.TodoItems.Single(x => x.Id == todoId);
            if (todoitemToUpdate == null)
            {
                return NotFound();
            }

            _dbContext.TodoItems.Remove(todoitemToUpdate);

            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {

            _logger.LogError($"Something happened: {ex}");
        }

        return Ok();
    }


    [HttpPut("{todoId}")]
    public async Task<IActionResult> UpdateTodoItem(Guid todolistId, Guid todoId, TodoItemDto newItem)
    {
        try
        {
            var todoitemToUpdate = _dbContext.TodoItems.Single(x => x.Id == todoId);
            if (todoitemToUpdate == null)
            {
                return NotFound();
            }

            todoitemToUpdate.Title = newItem.Title;
            todoitemToUpdate.Description = newItem.Description;
            todoitemToUpdate.IsDone = newItem.IsDone;

            _dbContext.TodoItems.Update(todoitemToUpdate);

            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {

            _logger.LogError($"Something happened: {ex}");
        }

        return Ok();
    }
}