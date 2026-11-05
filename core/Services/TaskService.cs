using core.Entities;
using core.Interfaces;
using core.Resources;

namespace core.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<List<TaskReadResource>> GetListAsync(TaskFilterResource filter)
        {
            var tasks = _taskRepository.GetAllTasks();

            return tasks.Select(t => new TaskReadResource
            {
                Id = t.Id,
                Title = t.Title,
                IsCompleted = t.IsCompleted
            }).ToList();
        }

        public async Task<List<TaskReadResource>> CreateAsync(TaskInsertResource[] resources)
        {
            var createdTasks = new List<TaskReadResource>();

            foreach (var res in resources)
            {
                var taskEntity = new TaskItem
                {
                    Title = res.Title,
                    IsCompleted = res.IsCompleted
                };

                // إزالة await واستدعاء AddTask مباشرة
                _taskRepository.AddTask(taskEntity);

                createdTasks.Add(new TaskReadResource
                {
                    Id = taskEntity.Id,
                    Title = taskEntity.Title,
                    IsCompleted = taskEntity.IsCompleted
                });
            }

            return createdTasks;
        }
    }
}