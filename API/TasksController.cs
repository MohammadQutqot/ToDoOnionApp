using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using core.Interfaces;
using core.Resources;

namespace API.Controllers
{
    [Authorize] 
    [ApiController]
    [Route("tasks")]
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

            var result = await _taskService.GetListAsync(userId, filterResource);
            return Ok(result);
        }

        [HttpPost("bulk")]
        public async Task<ActionResult<List<TaskReadResource>>> BulkPost([FromBody] TaskInsertResource[] resources)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

            var result = await _taskService.CreateAsync(userId, resources);
            return Ok(result);
        }
    }
}