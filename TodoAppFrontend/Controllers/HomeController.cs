using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using TodoAppFrontend.Models;
using TodoAppFrontend.Services;
using TodoAppShared;

namespace TodoAppFrontend.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ITodoService _todoService;

        public HomeController(IAuthService authService, ITodoService todoService) : base(authService)
        {
            _todoService = todoService;
        }

        public async Task<ActionResult> Index()
        {
            if (!AuthService.IsLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }

            var currentUser = AuthService.GetCurrentUser();
            var model = new HomeIndexViewModel
            {
                CurrentUserId = currentUser.UserID,
                CurrentUserName = currentUser.UserName
            };

            var result = await _todoService.GetTodoItemsByUserId(currentUser.UserID);
            if (result.IsSuccessful && result.Data != null)
            {
                model.TodoItems = result.Data.Select(todo => new TodoItemViewModel
                {
                    Id = todo.Id,
                    Title = todo.Title,
                    Description = todo.Description,
                    IsCompleted = todo.IsCompleted
                }).ToList();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CreateTodo(CreateTodoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid input" });
            }

            var result = await _todoService.CreateTodoItem(model.Title, model.Description);
            
            if (result.IsSuccessful && result.Data != null)
            {
                return Json(new { 
                    success = true, 
                    todo = new TodoItemViewModel 
                    { 
                        Id = result.Data.Id,
                        Title = result.Data.Title,
                        Description = result.Data.Description,
                        IsCompleted = result.Data.IsCompleted
                    }
                });
            }

            return Json(new { success = false, message = result.ErrorMessage ?? "Failed to create todo" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> UpdateTodo(string id, string title, string description)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(title))
            {
                return Json(new { success = false, message = "Id and Title are required" });
            }

            var currentUser = AuthService.GetCurrentUser();
            
            // Get the existing todo to preserve the IsCompleted status
            var existingTodoResult = await _todoService.GetTodoItemById(id);
            if (!existingTodoResult.IsSuccessful || existingTodoResult.Data == null)
            {
                return Json(new { success = false, message = "Todo not found" });
            }

            var todoDto = new TodoItemDTO
            {
                Id = id,
                Title = title,
                Description = description,
                IsCompleted = existingTodoResult.Data.IsCompleted, // Preserve existing status
                UserID = currentUser.UserID
            };

            var result = await _todoService.UpdateTodoItem(todoDto);
            
            if (result.IsSuccessful && result.Data != null)
            {
                return Json(new { 
                    success = true, 
                    todo = new TodoItemViewModel 
                    { 
                        Id = result.Data.Id,
                        Title = result.Data.Title,
                        Description = result.Data.Description,
                        IsCompleted = result.Data.IsCompleted
                    }
                });
            }

            return Json(new { success = false, message = result.ErrorMessage ?? "Failed to update todo" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteTodo(string id)
        {
            var result = await _todoService.DeleteTodoItem(id);
            
            if (result.IsSuccessful)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false, message = result.ErrorMessage ?? "Failed to delete todo" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ToggleTodo(string id, bool isCompleted)
        {
            var currentUser = AuthService.GetCurrentUser();
            var todoResult = await _todoService.GetTodoItemById(id);
            
            if (!todoResult.IsSuccessful || todoResult.Data == null)
            {
                return Json(new { success = false, message = "Todo not found" });
            }

            var todo = todoResult.Data;
            todo.IsCompleted = isCompleted;

            var result = await _todoService.UpdateTodoItem(todo);
            
            if (result.IsSuccessful)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false, message = result.ErrorMessage ?? "Failed to toggle todo" });
        }

        public ActionResult About()
        {
            if (!AuthService.IsLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            if (!AuthService.IsLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}
