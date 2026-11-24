using core.Entities;
using core.Resources;

namespace core.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskReadResource>> GetListAsync(TaskFilterResource filter);
        Task<List<TaskReadResource>> CreateAsync(TaskInsertResource[] resources);
        Task<TaskReadResource?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, TaskUpdateResource updateResource);
        Task<bool> DeleteAsync(int id);
    }
}