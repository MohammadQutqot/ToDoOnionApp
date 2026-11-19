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
        private readonly ICurrentUserService _currentUserService;

        public TaskService(
            IRepository<TaskItem> repository,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<List<TaskReadResource>> GetListAsync(TaskFilterResource filter)
        {
            var userId = _currentUserService.UserId;
            var tasks = await _repository.GetAllAsync();
            return _mapper.Map<List<TaskReadResource>>(tasks.Where(t => t.UserId == userId).ToList());
        }

        public async Task<List<TaskReadResource>> CreateAsync(TaskInsertResource[] resources)
        {
            var userId = _currentUserService.UserId;
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

        public async Task<TaskReadResource?> GetByIdAsync(int id)
        {
            var userId = _currentUserService.UserId;
            var task = await _repository.GetByIdAsync(id);
            return task?.UserId == userId ? _mapper.Map<TaskReadResource>(task) : null;
        }

        public async Task<bool> UpdateAsync(int id, TaskUpdateResource updateResource)
        {
            var userId = _currentUserService.UserId;
            var task = await _repository.GetByIdAsync(id);
            if (task == null || task.UserId != userId) return false;
            _mapper.Map(updateResource, task);
            _repository.Update(task);
            await _repository.SaveAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var userId = _currentUserService.UserId;
            var task = await _repository.GetByIdAsync(id);
            if (task == null || task.UserId != userId) return false;
            _repository.Delete(task);
            await _repository.SaveAsync();
            return true;
        }
    }
}