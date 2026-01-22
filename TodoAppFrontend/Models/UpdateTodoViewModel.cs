using System.ComponentModel.DataAnnotations;

namespace TodoAppFrontend.Models
{
    public class UpdateTodoViewModel
    {
        [Required]
        public string Id { get; set; }
        
        [Required(ErrorMessage = "Title is required")]
        [Display(Name = "Title")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; }
        
        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }
        
        [Display(Name = "Completed")]
        public bool IsCompleted { get; set; }
    }
}
