using System.Net;
using ElderlyCareSupport.Application.Common;
using ElderlyCareSupport.Application.Contracts;
using ElderlyCareSupport.Application.Contracts.Login;
using ElderlyCareSupport.Application.IService;
using ElderlyCareSupport.Application.Validation.AuthenticationValidators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElderlyCareSupport.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ElderlyCareSupportAccountController : ControllerBase
    {
        private readonly IFeeService _feeService;
        private readonly ILogger<ElderlyCareSupportAccountController> _logger;
        private readonly ILoginService _loginService;
        private readonly IRegistrationService _registrationService;
        private readonly IForgotPaswordService _forgotPasswordServicesWordService;
        private readonly IApiResponseFactoryService _aPiResponseFactoryService;
        private readonly IModelValidatorService _modelValidatorService;
        private readonly IValidator<RegistrationRequest> _validator;
        private readonly IValidator<LoginRequest> _loginValidator;
        private readonly IValidator<string> _emailValidator;

        public ElderlyCareSupportAccountController(IFeeService feeService,
            ILogger<ElderlyCareSupportAccountController> logger, ILoginService loginService,
            IRegistrationService registrationService, IForgotPaswordService forgotPasswordServicesWordService,
            IApiResponseFactoryService aPiResponseFactoryService, IModelValidatorService modelValidatorService,
            IValidator<RegistrationRequest> validator, IValidator<LoginRequest> loginValidator,
            IValidator<string> emailValidator)
        {
            _feeService = feeService;
            _logger = logger;
            _loginService = loginService;
            _registrationService = registrationService;
            _forgotPasswordServicesWordService = forgotPasswordServicesWordService;
            _aPiResponseFactoryService = aPiResponseFactoryService;
            _modelValidatorService = modelValidatorService;
            _validator = validator;
            _loginValidator = loginValidator;
            _emailValidator = emailValidator;
        }

        [HttpGet(nameof(GetFeeDetails))]
        public async Task<IActionResult> GetFeeDetails()
        {
            var feeConfigurationDto = await _feeService.GetAllFeeDetails();
            if (feeConfigurationDto.Count != 0)
            {
                _logger.LogInformation(
                    $"Data Couldn't be fetched from the server...\nClass: {nameof(ElderlyCareSupportAccountController)} Method: {nameof(GetFeeDetails)}");
                return Ok(_aPiResponseFactoryService.CreateResponse(success: false,
                    statusMessage: CommonConstants.StatusMessageNotFound, data: feeConfigurationDto,
                    code: HttpStatusCode.NotFound,
                    errorMessage: string.Format(CommonConstants.NotFound, "Fee Details")));
            }

            _logger.LogInformation(
                $"Data Successfully fetched from the server...\nClass: {nameof(ElderlyCareSupportAccountController)} Method: {nameof(GetFeeDetails)}");
            return Ok(_aPiResponseFactoryService.CreateResponse(success: true,
                statusMessage: CommonConstants.StatusMessageOk, code: HttpStatusCode.OK,
                data: feeConfigurationDto));
        }

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var validationResult = await _loginValidator.ValidateAsync(loginRequest);
            if (!validationResult.IsValid)
                return BadRequest(_modelValidatorService.ValidateModelState(validationResult.Errors));

            var result = await _loginService.AuthenticateLogin(loginRequest);

            if (result.Item2 is false)
                return Unauthorized(_aPiResponseFactoryService.CreateResponse(data: result,
                    success: result.Item2,
                    statusMessage: CommonConstants.StatusMessageNotFound,
                    code: HttpStatusCode.Unauthorized,
                    errorMessage: "User Not Found"));

            if (string.IsNullOrEmpty(result.Item1?.AccessToken) && result.Item2)
            {
                return Ok(_aPiResponseFactoryService.CreateResponse(data: result, success: false,
                    statusMessage: CommonConstants.StatusMessageNotFound,
                    code: HttpStatusCode.InternalServerError,
                    errorMessage: "Can't Get token from the Server..."));
            }

            return Ok(_aPiResponseFactoryService.CreateResponse(data: result, success: true,
                code: HttpStatusCode.OK,
                statusMessage: CommonConstants.StatusMessageOk));
        }

        [AllowAnonymous]
        [HttpPost(nameof(RegisterUser))]
        public async Task<IActionResult> RegisterUser([FromBody] RegistrationRequest registerRequest)
        {
            var validationResult = await _validator.ValidateAsync(registerRequest);
            if (!validationResult.IsValid)
                return BadRequest(_modelValidatorService.ValidateModelState(validationResult.Errors));

            var result = await _registrationService.CheckUserExistingAlready(registerRequest.Email);
            if (result)
            {
                return Conflict(_aPiResponseFactoryService.CreateResponse(data: Array.Empty<string>(), success: false,
                    statusMessage: CommonConstants.UserAlreadyExisted,
                    code: HttpStatusCode.Conflict,
                    error: [new Error { ErrorName = CommonConstants.UserAlreadyExisted }]));
            }

            var registrationStatus = await _registrationService.RegisterUserAsync(registerRequest);

            return registrationStatus
                ? Created(string.Empty, _aPiResponseFactoryService.CreateResponse(data: Array.Empty<string>(),
                    success: true,
                    code: HttpStatusCode.Created,
                    statusMessage: CommonConstants.StatusMessageOk))
                : StatusCode((int)HttpStatusCode.InternalServerError, _aPiResponseFactoryService.CreateResponse(
                    data: registrationStatus.ToString(), success: false,
                    statusMessage: string.Format(CommonConstants.OperationFailedErrorMessage, nameof(RegisterUser)),
                    code: HttpStatusCode.InternalServerError
                ));
        }

        [AllowAnonymous]
        [HttpGet($"{nameof(ForgotPassword)}/{{emailId}}")]
        public async Task<IActionResult> ForgotPassword(string emailId)
        {
            var validationResult = await _emailValidator.ValidateAsync(emailId);
            if (!validationResult.IsValid)
                return BadRequest(_modelValidatorService.ValidateModelState(validationResult.Errors));


            var result = await _forgotPasswordServicesWordService.GetForgotPassword(emailId);
            return Ok(string.IsNullOrEmpty(result)
                ? _aPiResponseFactoryService.CreateResponse(data: result, success: false,
                    code: HttpStatusCode.NotFound,
                    statusMessage: CommonConstants.StatusMessageNotFound, error:
                    [
                        new Error(errorName: string.Format(CommonConstants.NotFound, nameof(User)))
                    ])
                : _aPiResponseFactoryService.CreateResponse(data: result, success: true,
                    code: HttpStatusCode.OK,
                    statusMessage: CommonConstants.StatusMessageOk));
        }
    }
}