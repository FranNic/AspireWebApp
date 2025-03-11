namespace AspireWebApp.Web;

using System.Threading;

using Todo.Application.DTOs;
using Todo.Domain;

public class TodoApiClient(HttpClient httpClient, ILogger<TodoApiClient> logger)
{
    public async Task<List<TodoListDto>> GetTodoListsAsync(CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<List<TodoListDto>>("api/todolist", cancellationToken);
    }

    public async Task<TodoListDto> GetTodoListAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<TodoListDto>($"api/todolist/{id}", cancellationToken);
    }

    public async Task CreateTodoItemAsync(Guid id, TodoItemDto todoItem, CancellationToken cancellationToken = default)
    {
        try
        {
            HttpResponseMessage result = await httpClient.PostAsJsonAsync($"api/todolist/{id}/todoitem", todoItem, cancellationToken);
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

    /// <summary>
    /// Create Todo list
    /// </summary>
    /// <param name="todoList">The TodoListDto object representing the todo list to create</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A task representing the asynchronous operation</returns>
    public async Task<TodoListDto?> CreateTodoListAsync(TodoListDto todoList, CancellationToken cancellationToken = default)
    {
        try
        {
            HttpResponseMessage result = await httpClient.PostAsJsonAsync("api/todolist/", todoList, cancellationToken);

            if (!result.IsSuccessStatusCode)
            {
                string errorMessage = await result.Content.ReadAsStringAsync();
                logger.LogError("Failed to add todo list. Status: {StatusCode}, Response: {ErrorMessage}",
                    result.StatusCode, errorMessage);
                return null;
            }

            TodoListDto? createdTodoList = await result.Content.ReadFromJsonAsync<TodoListDto>(cancellationToken: cancellationToken);

            if (createdTodoList == null)
            {
                logger.LogWarning("Received null response when creating a todo list.");
            }

            return createdTodoList;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while creating the todo list.");
            throw; // Keep throwing so the caller can handle it
        }
    }


    public async Task<bool> DeleteTodoItemAsync(Guid listId, Guid todoItemId, CancellationToken cancellationToken = default)
    {
        try
        {
            HttpResponseMessage result = await httpClient.DeleteAsync($"api/todolist/{listId}/todoitem/{todoItemId}", cancellationToken);
            if (!result.IsSuccessStatusCode)
            {
                logger.LogError("Failed to delete todo item {todoItemId}", todoItemId);
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<bool> DeleteTodoListAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            HttpResponseMessage result = await httpClient.DeleteAsync($"api/todolist/{id}", cancellationToken);
            if (!result.IsSuccessStatusCode)
            {
                logger.LogError("Failed to delete todo list {id}", id);
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<TodoItemDto?> UpdateTodoItemAsync(Guid id, TodoItemDto todoItem, CancellationToken cancellationToken = default)
    {
        try
        {
            HttpResponseMessage result = await httpClient.PutAsJsonAsync($"api/todolist/{id}/todoitem/{todoItem.Id}", todoItem, cancellationToken);
            if (!result.IsSuccessStatusCode)
            {
                logger.LogError("Failed to update todo item {id}", id);
                return null;
            }

            return todoItem;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<TodoItemDto> GetTodoItemAsync(Guid listId, Guid todoItemId, CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<TodoItemDto>($"api/todolist/{listId}/todoitem/{todoItemId}", cancellationToken);
    }
}