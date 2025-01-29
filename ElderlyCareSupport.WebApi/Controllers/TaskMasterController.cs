using System.Net;
using ElderlyCareSupport.Application.Common;
using ElderlyCareSupport.Application.Contracts.Response;
using ElderlyCareSupport.Application.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElderlyCareSupport.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
// [Authorize]
public class TaskMasterController : ControllerBase
{
    private readonly ITaskMaster _taskMaster;
    private readonly IApiResponseFactoryService _apiResponseFactory;
    
    public TaskMasterController(ITaskMaster taskMaster, IApiResponseFactoryService apiResponseFactory)
    {
        _taskMaster = taskMaster;
        _apiResponseFactory = apiResponseFactory;
    }

    [HttpGet("task-categories")]
    public async Task<IActionResult> GetTaskCategories()
    {
        var taskCategories = await _taskMaster.GetTaskCategories();
        var response = _apiResponseFactory.CreateResponse(success:true, code:HttpStatusCode.OK, data:taskCategories, statusMessage: Constants.StatusMessageOk);
        return Ok(response);
    }
}