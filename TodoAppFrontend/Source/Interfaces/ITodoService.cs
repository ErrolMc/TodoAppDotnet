using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TodoAppFrontend.Source;
using TodoAppShared;

namespace TodoAppFrontend.Services
{
    public interface ITodoService
    {
        Task<Result<List<TodoItemDTO>>> GetTodoItemsByUserId(string userId);
        Task<Result<TodoItemDTO>> GetTodoItemById(string itemId);
        Task<Result<TodoItemDTO>> CreateTodoItem(string title, string description);
        Task<Result<TodoItemDTO>> UpdateTodoItem(TodoItemDTO todoItem);
        Task<Result> DeleteTodoItem(string itemId);
        Task<Result> DeleteAllTodoItemsForUser(string userId);
    }
}