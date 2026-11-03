using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infra;
using core.ToDo.Domain;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> GetTasks([FromQuery] bool? isCompleted, [FromQuery] string? search, [FromQuery] string role, [FromQuery] string userId)
        {
            var query = _context.TaskItems.AsQueryable();
            if (role != "Admin")
            {
                query = query.Where(t => t.UserId == userId);
            }

            if (isCompleted.HasValue)
            {
                query = query.Where(t => t.IsCompleted == isCompleted.Value);
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(t => t.Title.Contains(search));
            }

            var tasks = await query.ToListAsync();
            return Ok(tasks);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskItem task)
        {
            if (id != task.Id)
            {
                return BadRequest("معرّف المهمة غير متطابق");
            }

            _context.Entry(task).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null)
            {
                return NotFound("المهمة غير موجودة");
            }

            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    } 
} 