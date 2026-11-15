using AutoMapper;
using core.Entities;
using core.Interfaces;
using core.Resources;

namespace core.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<TaskItem> _repository;
        private readonly IMapper _mapper;

        public TaskService(IRepository<TaskItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<TaskReadResource>> GetListAsync(string userId, TaskFilterResource filter)
        {
            var tasks = await _repository.GetAllAsync();
            return _mapper.Map<List<TaskReadResource>>(tasks.Where(t => t.UserId == userId).ToList());
        }

        public async Task<List<TaskReadResource>> CreateAsync(string userId, TaskInsertResource[] resources)
        {
            var createdTasks = new List<TaskItem>();
            foreach (var res in resources)
            {
                var task = new TaskItem { Title = res.Title, IsCompleted = res.IsCompleted, UserId = userId };
                await _repository.AddAsync(task);
                createdTasks.Add(task);
            }
            await _repository.SaveAsync();
            return _mapper.Map<List<TaskReadResource>>(createdTasks);
        }

        public async Task<TaskReadResource?> GetByIdAsync(string userId, int id)
        {
            var task = await _repository.GetByIdAsync(id);
            return task?.UserId == userId ? _mapper.Map<TaskReadResource>(task) : null;
        }
        public async Task<bool> UpdateAsync(string userId, int id, TaskUpdateResource updateResource)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null || task.UserId != userId) return false;
            _mapper.Map(updateResource, task);
            _repository.Update(task);
            await _repository.SaveAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(string userId, int id)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null || task.UserId != userId) return false;
            _repository.Delete(task);
            await _repository.SaveAsync();
            return true;
        }
    }
}