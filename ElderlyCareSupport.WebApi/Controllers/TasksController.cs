using System.Net;
using ElderlyCareSupport.Application.Common;
using ElderlyCareSupport.Application.Contracts.Requests;
using ElderlyCareSupport.Application.Contracts.Response;
using ElderlyCareSupport.Application.IService;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElderlyCareSupport.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly IValidator<TaskCreationRequest> _validator;
    private readonly ITaskService _taskService;
    private readonly IApiResponseFactoryService _responseFactory;
    private readonly IModelValidatorService _modelValidatorService;

    public TasksController(IValidator<TaskCreationRequest> validator, ITaskService taskService,
        IApiResponseFactoryService responseFactory, IModelValidatorService modelValidatorService)
    {
        _validator = validator;
        _taskService = taskService;
        _responseFactory = responseFactory;
        _modelValidatorService = modelValidatorService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] TaskCreationRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return BadRequest(_modelValidatorService.ValidateModelState(validationResult.Errors));

        var result = await _taskService.CreateTask(request);
        if (!result)
            return BadRequest(result);

        return Created(RouteData.ToString(), result);
    }


    [HttpGet("{userId}")]
    public async Task<IActionResult> GetTasks(long userId, [FromQuery] TaskQueryParameters request)
    {
        //var validationResult = await _queryValidator.ValidateAsync(request);
        // if(!validationResult.IsValid)
        //     return BadRequest(_modelValidatorService.ValidateModelState(validationResult.Errors));
        //
        var result = await _taskService.GetTasks(userId, request);
        return Ok(result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] TaskCreationRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return BadRequest(_modelValidatorService.ValidateModelState(validationResult.Errors));

        var result = await _taskService.UpdateTask(request);
        if (result.Item2)
            return Ok(
                _responseFactory.CreateResponse(
                    statusMessage: Constants.OperationFailedErrorMessage,
                    success: result.Item2,
                    code: HttpStatusCode.NoContent,
                    data: Enumerable.Empty<string>(),
                    errorMessage: "Can't Update Task ")
            );

        return Ok(_responseFactory.CreateResponse(
                success: result.Item2,
                data: result,
                statusMessage: Constants.StatusMessageOk,
                code: HttpStatusCode.NoContent
            )
        );
    }
}