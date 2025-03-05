namespace AspireWebApp.Web;

using Todo.Application.DTOs;

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

    // Post method add todoitem to the list
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
    public async Task CreateTodoListAsync(TodoListDto todoList, CancellationToken cancellationToken = default)
    {
        try
        {
            HttpResponseMessage result = await httpClient.PostAsJsonAsync($"api/todolist/", todoList, cancellationToken);
            if (!result.IsSuccessStatusCode)
            {
                logger.LogError("Failed to add todo item to list {id}", todoList.Id);
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

    //add update
    public async Task UpdateTodoItemAsync(Guid id, TodoItemDto todoItem, CancellationToken cancellationToken = default)
    {
        try
        {
            HttpResponseMessage result = await httpClient.PutAsJsonAsync($"api/todolist/{id}/todoitem/{todoItem.Id}", todoItem, cancellationToken);
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

    public async Task<TodoItemDto> GetTodoItemAsync(Guid listId, Guid todoItemId, CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<TodoItemDto>($"api/todolist/{listId}/todoitem/{todoItemId}", cancellationToken);
    }
}