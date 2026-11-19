using System.ComponentModel.DataAnnotations.Schema;

namespace core.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }

        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }
        public int? CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }
        public TaskReminder? Reminder { get; set; }
        public ICollection<TaskTag> TaskTags { get; set; } = new List<TaskTag>();
    }
}