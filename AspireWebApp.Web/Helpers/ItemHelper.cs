namespace AspireWebApp.Web.Helpers;

using Todo.Application.DTOs;

public static class ItemHelper
{
    public static string Url(TodoListDto todoList)
        => $"todo/{todoList.Id}";
}