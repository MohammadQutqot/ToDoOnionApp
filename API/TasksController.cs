using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using core.Interfaces;
using core.Resources;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpPost("list")]
        public async Task<ActionResult<List<TaskReadResource>>> GetList([FromBody] TaskFilterResource filterResource)
        {
            var userId = GetUserId();
            var result = await _taskService.GetListAsync(userId, filterResource);
            return Ok(result);
        }

        [HttpPost("bulk")]
        public async Task<ActionResult<List<TaskReadResource>>> BulkPost([FromBody] TaskInsertResource[] resources)
        {
            var userId = GetUserId();
            var result = await _taskService.CreateAsync(userId, resources);
            return Ok(result);
        }

        // --- حط الـ Endpoints الجدد هون ---

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskReadResource>> GetById(int id)
        {
            var userId = GetUserId();
            var task = await _taskService.GetByIdAsync(userId, id);

            if (task == null) return NotFound();

            return Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] TaskUpdateResource updateResource)
        {
            var userId = GetUserId();
            var success = await _taskService.UpdateAsync(userId, id, updateResource);

            if (!success) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = GetUserId();
            var success = await _taskService.DeleteAsync(userId, id);

            if (!success) return NotFound();

            return NoContent();
        }
    }
}