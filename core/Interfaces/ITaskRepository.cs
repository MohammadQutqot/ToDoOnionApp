using core.Entities;

namespace core.Interfaces;

public interface ITaskRepository
{
    Task<IReadOnlyList<TaskItem>> GetListAsync(string userId, string? title, bool? isCompleted);
    Task<TaskItem?> GetByIdAsync(int id);
    Task AddAsync(TaskItem task);
    Task UpdateAsync(TaskItem task);
    Task DeleteAsync(TaskItem task);
}