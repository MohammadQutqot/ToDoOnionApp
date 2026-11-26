
using AutoMapper;
using core.Entities;
using core.Interfaces;
using core.Resources;

namespace core.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IRepository<TaskItem> _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public TaskService(
            ITaskRepository taskRepository,
            IRepository<TaskItem> repository,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _taskRepository = taskRepository;
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        private async Task<TaskItem?> GetUserTaskAsync(int id)
        {
            var userId = _currentUserService.UserId;

            if (string.IsNullOrEmpty(userId))
                return null;

            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null || task.UserId != userId)
                return null;

            return task;
        }

        public async Task<(List<TaskReadResource> Items, int TotalCount)> GetListAsync(
            TaskFilterResource filter)
        {
            var userId = _currentUserService.UserId;

            if (string.IsNullOrEmpty(userId))
                return (new List<TaskReadResource>(), 0);

            var pageNumber = filter.PageNumber < 1 ? 1 : filter.PageNumber;
            var pageSize = filter.PageSize < 1 ? 10 : filter.PageSize;

            var (tasks, totalCount) = await _taskRepository.GetListAsync(
                userId,
                filter.Title,
                filter.IsCompleted,
                filter.SortBy,
                pageNumber,
                pageSize);

            var items = _mapper.Map<List<TaskReadResource>>(tasks);

            return (items, totalCount);
        }

        public async Task<List<TaskReadResource>> CreateAsync(
            TaskInsertResource[] resources)
        {
            var userId = _currentUserService.UserId;
            var createdTasks = new List<TaskItem>();

            foreach (var res in resources)
            {
                var task = new TaskItem
                {
                    Title = res.Title,
                    IsCompleted = res.IsCompleted,
                    UserId = userId
                };

                await _taskRepository.AddAsync(task);
                createdTasks.Add(task);
            }

            await _repository.SaveAsync();

            return _mapper.Map<List<TaskReadResource>>(createdTasks);
        }

        public async Task<TaskReadResource?> GetByIdAsync(int id)
        {
            var task = await GetUserTaskAsync(id);

            return task == null
                ? null
                : _mapper.Map<TaskReadResource>(task);
        }

        public async Task<bool> UpdateAsync(
            int id,
            TaskUpdateResource updateResource)
        {
            var task = await GetUserTaskAsync(id);

            if (task == null)
                return false;

            _mapper.Map(updateResource, task);

            await _taskRepository.UpdateAsync(task);
            await _repository.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await GetUserTaskAsync(id);

            if (task == null)
                return false;

            await _taskRepository.DeleteAsync(task);
            await _repository.SaveAsync();

            return true;
        }
    }
}
