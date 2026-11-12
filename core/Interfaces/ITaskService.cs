using core.Resources;

namespace core.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskReadResource>> GetListAsync(string userId, TaskFilterResource filter);
        Task<List<TaskReadResource>> CreateAsync(string userId, TaskInsertResource[] resources);
    }
}