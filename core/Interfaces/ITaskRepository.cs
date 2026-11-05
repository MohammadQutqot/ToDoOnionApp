using core.Entities;

namespace core.Interfaces;

public interface ITaskRepository
{
    List<TaskItem> GetAllTasks();
    void AddTask(TaskItem task);
}