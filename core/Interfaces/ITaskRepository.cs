using core.Entities;

namespace core.Interfaces;

public interface ITaskRepository : IRepository<TaskItem>
{
    Task<(IReadOnlyList<TaskItem> Items, int TotalCount)> GetListAsync(
        string userId,
        string? title,
        bool? isCompleted,
        string? sortBy,
        int pageNumber,
        int pageSize);

    Task UpdateAsync(TaskItem task);
    Task DeleteAsync(TaskItem task);
}