using System.Net;
using ElderlyCareSupport.Application.Common;
using ElderlyCareSupport.Application.Contracts.Requests;
using ElderlyCareSupport.Application.Contracts.Response;
using ElderlyCareSupport.Application.IService;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElderlyCareSupport.Server.Controllers;

[Route("auth/[controller]")]
[ApiController]
[Produces("application/json")]
public class AccountsController : ControllerBase
{
    private readonly IValidator<LoginRequest> _loginValidator;
    private readonly IModelValidatorService _modelValidatorService;
    private readonly ILoginService _loginService;
    private readonly IApiResponseFactoryService _aPiResponseFactoryService;
    private readonly IRegistrationService _registrationService;
    private readonly IForgotPaswordService _forgotPasswordServicesWordService;
    private readonly IValidator<RegistrationRequest> _validator;
    private readonly IValidator<string> _emailValidator;


    public AccountsController(IValidator<LoginRequest> loginValidator, IModelValidatorService modelValidatorService,
        ILoginService loginService, IApiResponseFactoryService aPiResponseFactoryService,
        IRegistrationService registrationService, IForgotPaswordService forgotPasswordServicesWordService,
        IValidator<RegistrationRequest> validator, IValidator<string> emailValidator)
    {
        _loginValidator = loginValidator;
        _modelValidatorService = modelValidatorService;
        _loginService = loginService;
        _aPiResponseFactoryService = aPiResponseFactoryService;
        _registrationService = registrationService;
        _forgotPasswordServicesWordService = forgotPasswordServicesWordService;
        _validator = validator;
        _emailValidator = emailValidator;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var validationResult = await _loginValidator.ValidateAsync(loginRequest);
        if (!validationResult.IsValid)
            return BadRequest(_modelValidatorService.ValidateModelState(validationResult.Errors));

        var result = await _loginService.AuthenticateLogin(loginRequest);

        if (!result.Item2)
            return Unauthorized(_aPiResponseFactoryService.CreateResponse(data: result,
                success: result.Item2,
                statusMessage: Constants.StatusMessageNotFound,
                code: HttpStatusCode.Unauthorized,
                errorMessage: "User Not Found"));

        if (string.IsNullOrEmpty(result.Item1?.AccessToken) && result.Item2)
        {
            return Ok(_aPiResponseFactoryService.CreateResponse(data: result, success: false,
                statusMessage: Constants.StatusMessageNotFound,
                code: HttpStatusCode.InternalServerError,
                errorMessage: "Can't Get token from the Server..."));
        }

        return Ok(_aPiResponseFactoryService.CreateResponse(data: result, success: true,
            code: HttpStatusCode.OK,
            statusMessage: Constants.StatusMessageOk));
    }


    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegistrationRequest registerRequest)
    {
        var validationResult = await _validator.ValidateAsync(registerRequest);
        if (!validationResult.IsValid)
            return BadRequest(_modelValidatorService.ValidateModelState(validationResult.Errors));

        var result = await _registrationService.CheckUserExistingAlready(registerRequest.Email);
        if (result)
        {
            return Conflict(_aPiResponseFactoryService.CreateResponse(data: Array.Empty<string>(), success: false,
                statusMessage: Constants.UserAlreadyExisted,
                code: HttpStatusCode.Conflict,
                error: [new Error { ErrorName = Constants.UserAlreadyExisted }]));
        }

        var registrationStatus = await _registrationService.RegisterUserAsync(registerRequest);

        return registrationStatus
            ? Created(string.Empty, _aPiResponseFactoryService.CreateResponse(data: Array.Empty<string>(),
                success: true,
                code: HttpStatusCode.Created,
                statusMessage: Constants.StatusMessageOk))
            : StatusCode((int)HttpStatusCode.InternalServerError, _aPiResponseFactoryService.CreateResponse(
                data: registrationStatus.ToString(), success: false,
                statusMessage: string.Format(Constants.OperationFailedErrorMessage, nameof(RegisterUser)),
                code: HttpStatusCode.InternalServerError
            ));
    }

    [AllowAnonymous]
    [HttpGet($"forgot-password/{{emailId}}")]
    public async Task<IActionResult> ForgotPassword(string emailId)
    {
        var validationResult = await _emailValidator.ValidateAsync(emailId);
        if (!validationResult.IsValid)
            return BadRequest(_modelValidatorService.ValidateModelState(validationResult.Errors));


        var result = await _forgotPasswordServicesWordService.GetForgotPassword(emailId);
        return Ok(string.IsNullOrEmpty(result)
            ? _aPiResponseFactoryService.CreateResponse(data: result, success: false,
                code: HttpStatusCode.NotFound,
                statusMessage: Constants.StatusMessageNotFound, error:
                [
                    new Error(errorName: string.Format(Constants.NotFound, nameof(User)))
                ])
            : _aPiResponseFactoryService.CreateResponse(data: result, success: true,
                code: HttpStatusCode.OK,
                statusMessage: Constants.StatusMessageOk));
    }
}