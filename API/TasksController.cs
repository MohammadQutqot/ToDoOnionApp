using Microsoft.AspNetCore.Mvc;
using core.Interfaces;
using core.Resources;

namespace API.Controllers
{
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
            var result = await _taskService.GetListAsync(filterResource);
            return Ok(result);
        }

        [HttpPost("bulk")]
        public async Task<ActionResult<List<TaskReadResource>>> BulkPost([FromBody] TaskInsertResource[] resources)
        {
            var result = await _taskService.CreateAsync(resources);
            return Ok(result);
        }
    }
}