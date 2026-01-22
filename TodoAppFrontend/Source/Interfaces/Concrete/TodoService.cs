using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using TodoAppFrontend.Http;
using TodoAppFrontend.Source;
using TodoAppShared;

namespace TodoAppFrontend.Services.Concrete
{
    public class TodoService : HttpService, ITodoService
    {
        private readonly IAuthService _authService;

        public TodoService(IAuthService authService) 
        {
            _authService = authService;
        }

        public async Task<Result<List<TodoItemDTO>>> GetTodoItemsByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return Result<List<TodoItemDTO>>.Failure("Invalid user ID");
            }

            ApiResponse<List<TodoItemDTO>> apiResponse = 
                await HttpHelper.GetAsync<List<TodoItemDTO>>(HttpClient, $"api/todoitems/{userId}");
            
            if (apiResponse.IsSuccessful)
                return Result<List<TodoItemDTO>>.Success(apiResponse.Data != null ? apiResponse.Data : new List<TodoItemDTO>());

            return Result<List<TodoItemDTO>>.Failure(apiResponse.HttpResponse.ReasonPhrase);
        }

        public async Task<Result<TodoItemDTO>> GetTodoItemById(string itemId)
        {
            if (string.IsNullOrEmpty(itemId))
            {
                return Result<TodoItemDTO>.Failure("Invalid item ID");
            }

            ApiResponse<TodoItemDTO> response = 
                await HttpHelper.GetAsync<TodoItemDTO>(HttpClient, $"api/todoitem/{itemId}");

            return response;
        }

        public async Task<Result<TodoItemDTO>> CreateTodoItem(string title, string description)
        {
            UserDTO currentUser = _authService.GetCurrentUser();
            if (currentUser == null || string.IsNullOrEmpty(title))
            {
                return Result<TodoItemDTO>.Failure(ResultType.InputError, "Invalid input");
            }

            var request = new CreateTodoItemRequest
            {
                UserID = currentUser.UserID,
                Title = title,
                Description = description
            };

            ApiResponse<TodoItemDTO> response = 
                await HttpHelper.PostAsync<CreateTodoItemRequest, TodoItemDTO>(HttpClient, "api/todoitem/create", request);

            return response;
        }

        public async Task<Result<TodoItemDTO>> UpdateTodoItem(TodoItemDTO todoItem)
        {
            if (todoItem == null || string.IsNullOrEmpty(todoItem.Id))
            {
                return Result<TodoItemDTO>.Failure(ResultType.InputError, "Invalid TodoItem ID");
            }

            ApiResponse<TodoItemDTO> response = 
                await HttpHelper.PutAsync<TodoItemDTO, TodoItemDTO>(HttpClient, "api/todoitem/update", todoItem);

            return response;
        }

        public async Task<Result> DeleteTodoItem(string itemId)
        {
            if (string.IsNullOrEmpty(itemId))
            {
                return Result.Failure(ResultType.InputError, "ItemID is null");
            }

            Result result = await HttpHelper.DeleteAsync(HttpClient, $"api/todoitem/delete/{itemId}");
            return result;
        }

        public async Task<Result> DeleteAllTodoItemsForUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return Result.Failure(ResultType.InputError, "UserID is null");
            }

            Result result = await HttpHelper.DeleteAsync(HttpClient, $"api/todoitems/deleteall/{userId}");
            return result;
        }
    }
}