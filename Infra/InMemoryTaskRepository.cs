using core.Entities;
using core.Interfaces;

namespace Infra;

public class InMemoryTaskRepository : ITaskRepository
{
    private readonly List<TaskItem> _tasks = new();

    public List<TaskItem> GetAllTasks()
    {
        return _tasks;
    }

    public void AddTask(TaskItem task)
    {
        task.Id = _tasks.Count > 0 ? _tasks.Max(t => t.Id) + 1 : 1;
        _tasks.Add(task);
    }
}