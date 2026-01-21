using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAppBackend.Data;

namespace TodoAppBackend.Repositories
{
    public interface ITodoItemRepository
    {
        Task<List<TodoItem>> GetTodoItemsByUserIdAsync(string userId);
        Task<bool> AddTodoItemAsync(TodoItem item);
        Task<bool> RemoveTodoItemAsync(string itemId);
        Task<bool> UpdateTodoItemAsync(TodoItem item);
        Task<TodoItem> GetTodoItemByIdAsync(string itemId);
        Task<bool> DeleteAllTodoItemsForUserIdAsync(string userId);
    }
}
