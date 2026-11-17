using System.ComponentModel.DataAnnotations;

namespace core.Resources
{
    public class TaskInsertResource
    {
        [Required(ErrorMessage = "Task title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
        public string Title { get; set; } = string.Empty; 
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }
    }
}