using core.ToDo.Domain;

namespace core;

public interface ITaskRepository
{
    List<TaskItem> GetAllTasks();
    void AddTask(TaskItem task);
}