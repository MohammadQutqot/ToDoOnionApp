namespace core.Entities
{
    public class TaskReminder
    {
        public int Id { get; set; }
        public DateTime RemindAt { get; set; }
        public bool IsSent { get; set; }

        public int TaskItemId { get; set; }
        public TaskItem TaskItem { get; set; } = null!;
    }
}