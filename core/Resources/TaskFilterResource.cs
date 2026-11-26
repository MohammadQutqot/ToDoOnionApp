
namespace core.Resources
{
    public class TaskFilterResource
    {
        public string? Title { get; set; }
        public bool? IsCompleted { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; }
    }
}
