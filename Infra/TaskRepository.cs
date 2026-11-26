
using core.Entities;
using core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(IReadOnlyList<TaskItem> Items, int TotalCount)> GetListAsync(
            string userId,
            string? title,
            bool? isCompleted,
            string? sortBy,
            int pageNumber,
            int pageSize)
        {
            var query = _context.Tasks
                .AsNoTracking()
                .Where(t => t.UserId == userId);

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(t => t.Title.Contains(title));
            }

            if (isCompleted.HasValue)
            {
                query = query.Where(t => t.IsCompleted == isCompleted.Value);
            }

            var totalCount = await query.CountAsync();

            query = sortBy?.ToLower() switch
            {
                "title" => query.OrderBy(t => t.Title),
                "title_desc" => query.OrderByDescending(t => t.Title),
                "date" => query.OrderBy(t => t.Id),
                "date_desc" => query.OrderByDescending(t => t.Id),
                _ => query.OrderBy(t => t.Id)
            };

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task AddAsync(TaskItem task)
        {
            await _context.Tasks.AddAsync(task);
        }

        public Task UpdateAsync(TaskItem task)
        {
            _context.Tasks.Update(task);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TaskItem task)
        {
            _context.Tasks.Remove(task);
            return Task.CompletedTask;
        }

        public async Task<List<TaskItem>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public void Update(TaskItem task)
        {
            _context.Tasks.Update(task);
        }

        public void Delete(TaskItem task)
        {
            _context.Tasks.Remove(task);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
