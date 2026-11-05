using core.Resources;

namespace core.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskReadResource>> GetListAsync(TaskFilterResource filter);
        Task<List<TaskReadResource>> CreateAsync(TaskInsertResource[] resources);
    }
}