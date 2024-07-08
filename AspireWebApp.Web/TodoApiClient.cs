namespace AspireWebApp.Web;

using Todo.Domain;

public class TodoApiClient(HttpClient httpClient)
{
    public async Task<IEnumerable<TodoList>> GetTodoListsAsync(CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<IEnumerable<TodoList>>("todolist", cancellationToken);
    }
}

