namespace AspireWebApp.Web.Helpers;

using Todo.Application.DTOs;

public static class ItemHelper
{
    public static string TodoListUrl(TodoListDto todoList)
        => $"todo/{todoList.Id}";

    public static string TodoItemUrl(TodoListDto todoList, TodoItemDto todoitem)
        => $"todo/{todoList.Id}/{todoitem.Id}";
}