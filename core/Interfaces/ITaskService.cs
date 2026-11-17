using core.Entities;
using core.Resources;

namespace core.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskReadResource>> GetListAsync(string userId, TaskFilterResource filter);
        Task<List<TaskReadResource>> CreateAsync(string userId, TaskInsertResource[] resources);
        Task<TaskReadResource?> GetByIdAsync(string userId, int id);
        Task<bool> UpdateAsync(string userId, int id, TaskUpdateResource updateResource);
        Task<bool> DeleteAsync(string userId, int id);
    }
}