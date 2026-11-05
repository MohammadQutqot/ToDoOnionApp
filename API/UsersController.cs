using Microsoft.AspNetCore.Mvc;
using core.Interfaces;
using core.Resources;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterResource resource)
        {
            var result = await _userService.RegisterAsync(resource);
            return Ok(result);
        }
    }
}