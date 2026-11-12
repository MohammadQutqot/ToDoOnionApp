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

        public async Task<List<TaskReadResource>> GetListAsync(string userId, TaskFilterResource filter)
        {
            var tasks = await _taskRepository.GetListAsync(userId, filter?.Title, filter?.IsCompleted);

            return tasks.Select(t => new TaskReadResource
            {
                Id = t.Id,
                Title = t.Title,
                IsCompleted = t.IsCompleted
            }).ToList();
        }

        public async Task<List<TaskReadResource>> CreateAsync(string userId, TaskInsertResource[] resources)
        {
            var createdTasks = new List<TaskReadResource>();

            foreach (var res in resources)
            {
                var taskEntity = new TaskItem
                {
                    Title = res.Title,
                    IsCompleted = res.IsCompleted,
                    UserId = userId 
                };

          
                await _taskRepository.AddAsync(taskEntity);

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