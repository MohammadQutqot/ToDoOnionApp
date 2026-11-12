using core.Entities;
using core.Interfaces;

namespace Infra;

public class InMemoryTaskRepository : ITaskRepository
{
    private readonly List<TaskItem> _tasks = new();

    public Task<IReadOnlyList<TaskItem>> GetListAsync(string userId, string? title, bool? isCompleted)
    {
        var query = _tasks.Where(t => t.UserId == userId);

        if (!string.IsNullOrWhiteSpace(title))
        {
            query = query.Where(t => t.Title != null && t.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
        }

        if (isCompleted.HasValue)
        {
            query = query.Where(t => t.IsCompleted == isCompleted.Value);
        }

        IReadOnlyList<TaskItem> result = query.ToList();
        return Task.FromResult(result);
    }

    public Task<TaskItem?> GetByIdAsync(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(task);
    }

    public Task AddAsync(TaskItem task)
    {
        task.Id = _tasks.Count > 0 ? _tasks.Max(t => t.Id) + 1 : 1;
        _tasks.Add(task);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(TaskItem task)
    {
        var existingTask = _tasks.FirstOrDefault(t => t.Id == task.Id);
        if (existingTask != null)
        {
            existingTask.Title = task.Title;
            existingTask.IsCompleted = task.IsCompleted;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TaskItem task)
    {
        _tasks.Remove(task);
        return Task.CompletedTask;
    }
}