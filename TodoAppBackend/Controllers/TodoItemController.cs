using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using TodoAppBackend.Data;
using TodoAppBackend.Repositories;
using TodoAppShared;

namespace TodoAppBackend.Controllers
{
    public class TodoItemController : ApiController
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public TodoItemController(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        [HttpGet]
        [Route("api/todoitems/{userId}")]
        public async Task<IHttpActionResult> GetTodoItemsByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is required.");
            }

            var items = await _todoItemRepository.GetTodoItemsByUserIdAsync(userId);
            var itemDtos = items.Select(item => item.ToDTO()).ToList();

            return Ok(itemDtos);
        }

        [HttpGet]
        [Route("api/todoitem/{itemId}")]
        public async Task<IHttpActionResult> GetTodoItemById(string itemId)
        {
            if (string.IsNullOrEmpty(itemId))
            {
                return BadRequest("Item ID is required.");
            }

            var item = await _todoItemRepository.GetTodoItemByIdAsync(itemId);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item.ToDTO());
        }

        [HttpPost]
        [Route("api/todoitem/create")]
        public async Task<IHttpActionResult> CreateTodoItem(CreateTodoItemRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.UserID) || string.IsNullOrEmpty(request.Title))
            {
                return BadRequest("User ID and Title are required");
            }

            var newItem = new TodoItem
            {
                Id = Guid.NewGuid().ToString(),
                UserID = request.UserID,
                Title = request.Title,
                Description = request.Description,
                CreateDate = DateTime.UtcNow,
                IsCompleted = false,
                CompletedDate = null,
                LastEditedDate = null
            };

            bool result = await _todoItemRepository.AddTodoItemAsync(newItem);

            if (!result)
            {
                return InternalServerError();
            }

            return Ok(newItem.ToDTO());
        }

        [HttpPut]
        [Route("api/todoitem/update")]
        public async Task<IHttpActionResult> UpdateTodoItem(TodoItemDTO todoItemDto)
        {
            if (todoItemDto == null || string.IsNullOrEmpty(todoItemDto.Id))
            {
                return BadRequest("Item ID is required.");
            }

            var existingItem = await _todoItemRepository.GetTodoItemByIdAsync(todoItemDto.Id);

            if (existingItem == null)
            {
                return NotFound();
            }

            var updatedItem = new TodoItem
            {
                Id = todoItemDto.Id,
                UserID = existingItem.UserID,
                Title = todoItemDto.Title ?? existingItem.Title,
                Description = todoItemDto.Description,
                CreateDate = existingItem.CreateDate,
                IsCompleted = todoItemDto.IsCompleted,
                CompletedDate = todoItemDto.CompletedDate,
                LastEditedDate = DateTime.UtcNow
            };

            if (!todoItemDto.IsCompleted)
            {
                updatedItem.CompletedDate = null;
            }
            else if (!existingItem.IsCompleted)
            {
                updatedItem.CompletedDate = DateTime.UtcNow;
            }

            bool result = await _todoItemRepository.UpdateTodoItemAsync(updatedItem);

            if (!result)
            {
                return InternalServerError();
            }

            return Ok(updatedItem.ToDTO());
        }

        [HttpDelete]
        [Route("api/todoitem/delete/{itemId}")]
        public async Task<IHttpActionResult> DeleteTodoItem(string itemId)
        {
            if (string.IsNullOrEmpty(itemId))
            {
                return BadRequest("Item ID is required.");
            }

            bool result = await _todoItemRepository.RemoveTodoItemAsync(itemId);

            if (!result)
            {
                return NotFound();
            }

            return Ok("Todo item deleted successfully.");
        }

        [HttpDelete]
        [Route("api/todoitems/deleteall/{userId}")]
        public async Task<IHttpActionResult> DeleteAllTodoItemsForUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is required.");
            }

            bool result = await _todoItemRepository.DeleteAllTodoItemsForUserIdAsync(userId);

            if (!result)
            {
                return InternalServerError();
            }

            return Ok("All todo items deleted successfully.");
        }
    }
}