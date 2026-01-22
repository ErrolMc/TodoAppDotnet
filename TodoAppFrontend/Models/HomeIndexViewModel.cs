using System.Collections.Generic;

namespace TodoAppFrontend.Models
{
    public class HomeIndexViewModel
    {
        public List<TodoItemViewModel> TodoItems { get; set; }
        public string CurrentUserId { get; set; }
        public string CurrentUserName { get; set; }

        public HomeIndexViewModel()
        {
            TodoItems = new List<TodoItemViewModel>();
        }
    }
}
