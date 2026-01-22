using System.ComponentModel.DataAnnotations;

namespace TodoAppFrontend.Models
{
    public class TodoItemViewModel
    {
        public string Id { get; set; }
        
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        
        [Display(Name = "Description")]
        public string Description { get; set; }
        
        [Display(Name = "Completed")]
        public bool IsCompleted { get; set; }
    }
}
