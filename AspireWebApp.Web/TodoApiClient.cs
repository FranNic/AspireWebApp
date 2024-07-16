﻿namespace AspireWebApp.Web;

using Todo.Application.DTOs;

public class TodoApiClient(HttpClient httpClient, ILogger<TodoApiClient> logger)
{
    public async Task<IEnumerable<TodoListDto>> GetTodoListsAsync(CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<IEnumerable<TodoListDto>>("api/todolist", cancellationToken);
    }

    public async Task<TodoListDto> GetTodoListAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<TodoListDto>($"api/todolist/{id}", cancellationToken);
    }

    // Post method add todoitem to the list
    public async Task AddTodoItemAsync(Guid id, TodoItemDto todoItem, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await httpClient.PostAsJsonAsync($"api/todolist/{id}/todoitem", todoItem, cancellationToken);
            if (!result.IsSuccessStatusCode)
            {
                logger.LogError("Failed to add todo item to list {id}", id);
                return;
            }

            return;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    // add delete
    public async Task DeleteTodoItemAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await httpClient.DeleteAsync($"api/todolist/{id}/todoitem", cancellationToken);
            if (!result.IsSuccessStatusCode)
            {
                logger.LogError("Failed to delete todo item {id}", id);
                return;
            }

            return;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    //add update
    public async Task UpdateTodoItemAsync(Guid id, TodoItemDto todoItem, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await httpClient.PutAsJsonAsync($"api/todolist/{id}/todoitem", todoItem, cancellationToken);
            if (!result.IsSuccessStatusCode)
            {
                logger.LogError("Failed to update todo item {id}", id);
                return;
            }

            return;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}