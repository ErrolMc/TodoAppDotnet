using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TodoAppBackend.Data;

namespace TodoAppBackend.Repositories.Concrete
{
    public class TodoItemRepository : ITodoItemRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed = false;

        public TodoItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TodoItem>> GetTodoItemsByUserIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return new List<TodoItem>();

            return await _context.TodoItems
                .Where(t => t.UserID == userId)
                .ToListAsync();
        }

        public async Task<TodoItem> GetTodoItemByIdAsync(string itemId)
        {
            if (string.IsNullOrEmpty(itemId))
                return null;

            return await _context.TodoItems
                .FirstOrDefaultAsync(t => t.Id == itemId);
        }

        public async Task<bool> AddTodoItemAsync(TodoItem item)
        {
            if (item == null || string.IsNullOrEmpty(item.Id))
                return false;

            try
            {
                _context.TodoItems.Add(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateTodoItemAsync(TodoItem item)
        {
            if (item == null || string.IsNullOrEmpty(item.Id))
                return false;

            try
            {
                TodoItem existingItem = await _context.TodoItems
                    .FirstOrDefaultAsync(t => t.Id == item.Id);

                if (existingItem == null)
                    return false;

                existingItem.Title = item.Title;
                existingItem.Description = item.Description;
                existingItem.IsCompleted = item.IsCompleted;
                existingItem.CompletedDate = item.CompletedDate;
                existingItem.LastEditedDate = item.LastEditedDate;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> RemoveTodoItemAsync(string itemId)
        {
            if (string.IsNullOrEmpty(itemId))
                return false;

            try
            {
                var item = await _context.TodoItems
                    .FirstOrDefaultAsync(t => t.Id == itemId);

                if (item == null)
                    return false;

                _context.TodoItems.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAllTodoItemsForUserIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return false;

            try
            {
                var items = _context.TodoItems
                    .Where(t => t.UserID == userId);

                if (!items.Any())
                    return true;

                _context.TodoItems.RemoveRange(items);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region disposing
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}