using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElderlyCareSupport.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TaskController : ControllerBase
    {
        [Authorize]
        [HttpGet($"{nameof(GetTaskDetailsById)}/{{guid}}")]
        public async Task<IActionResult> GetTaskDetailsById(Guid guid)
        {
            return await Task.FromResult(Ok());
        }
    }
}