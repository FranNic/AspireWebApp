﻿namespace Todo.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;

using Todo.Application.DTOs;
using Todo.Domain;
using Todo.Infrastructure.Persistence;

[Route("api/todolist/{todolistId}/[controller]")]
[ApiController]
public class TodoItemController : ControllerBase
{
    private readonly TodoDbContext _dbContext;
    private readonly ILogger<TodoItemController> _logger;

    public TodoItemController(TodoDbContext dbContext, ILogger<TodoItemController> logger)
    {
        this._dbContext = dbContext;
        this._logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodoItem(Guid todolistId, TodoItemDto newItem)
    {
        try
        {
            var todolist = await _dbContext.TodoLists.AsNoTracking().FirstOrDefaultAsync(x => x.Id == todolistId);
            if (todolist == null)
            {
                return NotFound();
            }
            var item = new TodoItem
            {
                Title = newItem.Title,
                Description = newItem.Title,
                Id = Guid.NewGuid(),
                ListId = todolistId
            };

            _dbContext.TodoItems.Add(item);


            await _dbContext.SaveChangesAsync();
            // return created
            return Created("GetById", item);

            
        }
        catch (Exception ex)
        {

            throw;
        }

    }

    private object GetById() => throw new NotImplementedException();
}
