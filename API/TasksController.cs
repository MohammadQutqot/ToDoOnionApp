using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("list")]
        public async Task<ActionResult<List<TaskReadResource>>> GetList([FromBody] TaskFilterResource filterResource)
        {
            var result = await _taskService.GetListAsync(filterResource);
            return Ok(result);
        }

        [HttpPost("bulk")]
        public async Task<ActionResult<List<TaskReadResource>>> BulkPost([FromBody] TaskInsertResource[] resources)
        {
            var result = await _taskService.CreateAsync(resources);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskReadResource>> GetById(int id)
        {
            var task = await _taskService.GetByIdAsync(id);

            if (task == null) return NotFound();

            return Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] TaskUpdateResource updateResource)
        {
            var success = await _taskService.UpdateAsync(id, updateResource);

            if (!success) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _taskService.DeleteAsync(id);

            if (!success) return NotFound();

            return NoContent();
        }
    }
}