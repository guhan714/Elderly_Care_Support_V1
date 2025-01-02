using System.Net;
using ElderlyCareSupport.Server.Common;
using ElderlyCareSupport.Server.Services.Interfaces;
using ElderlyCareSupport.Server.ViewModels;
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

        public ElderlyCareSupportAccountController(IFeeService feeService,
            ILogger<ElderlyCareSupportAccountController> logger, ILoginService loginService,
            IRegistrationService registrationService, IForgotPaswordService forgotPasswordServicesWordService,
            IApiResponseFactoryService aPiResponseFactoryService, IModelValidatorService modelValidatorService)
        {
            _feeService = feeService;
            _logger = logger;
            _loginService = loginService;
            _registrationService = registrationService;
            _forgotPasswordServicesWordService = forgotPasswordServicesWordService;
            _aPiResponseFactoryService = aPiResponseFactoryService;
            _modelValidatorService = modelValidatorService;
        }

        [HttpGet(nameof(GetFeeDetails))]
        public async Task<IActionResult> GetFeeDetails()
        {
            var feeConfigurationDto = await _feeService.GetAllFeeDetails();
            feeConfigurationDto = feeConfigurationDto.ToArray();
            if (feeConfigurationDto.Any())
            {
                _logger.LogInformation(
                    $"Data Successfully fetched from the server...\nClass: {nameof(ElderlyCareSupportAccountController)} Method: {nameof(GetFeeDetails)}");
                return Ok(_aPiResponseFactoryService.CreateResponse(success: true,
                    statusMessage: CommonConstants.StatusMessageOk, code: HttpStatusCode.OK,
                    data: feeConfigurationDto));
            }

            _logger.LogInformation(
                $"Data Couldn't be fetched from the server...\nClass: {nameof(ElderlyCareSupportAccountController)} Method: {nameof(GetFeeDetails)}");
            return Ok(_aPiResponseFactoryService.CreateResponse(success: false,
                statusMessage: CommonConstants.StatusMessageNotFound, data: feeConfigurationDto,
                code: HttpStatusCode.NotFound,
                errorMessage: string.Format(CommonConstants.NotFound, "Fee Details")));
        }

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = _modelValidatorService.ValidateModelState(ModelState);
                return Ok(errorMessage);
            }

            var result = await _loginService.AuthenticateLogin(loginViewModel);

            if (result?.Item2 is false)
                return Unauthorized(_aPiResponseFactoryService.CreateResponse(data: result,
                    success: result!.Item2,

                    statusMessage: CommonConstants.StatusMessageNotFound,
                    code: HttpStatusCode.Unauthorized,
                    errorMessage: "User Not Found"));

            if (string.IsNullOrEmpty(result.Item1?.AccessToken) && result.Item2 is true)
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
        public async Task<IActionResult> RegisterUser([FromBody] RegistrationViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = _modelValidatorService.ValidateModelState(ModelState);
                return Ok(errorMessage);
            }

            var result = _registrationService.CheckUserExistingAlready(registerViewModel.Email);
            if (result.Result)
            {
                return Ok(_aPiResponseFactoryService.CreateResponse(data: Array.Empty<string>(), success: false,
                    statusMessage: CommonConstants.UserAlreadyExisted,
                    code: HttpStatusCode.OK,
                    error: [new Error { ErrorName = CommonConstants.UserAlreadyExisted }]));
            }

            var registrationStatus = await _registrationService.RegisterUserAsync(registerViewModel);

            return Ok(registrationStatus
                ? _aPiResponseFactoryService.CreateResponse(data: Array.Empty<string>(), success: true,
                    code: HttpStatusCode.Created,
                    statusMessage: CommonConstants.StatusMessageOk)
                : _aPiResponseFactoryService.CreateResponse(data: registrationStatus.ToString(), success: false,
                    statusMessage: string.Format(CommonConstants.OperationFailedErrorMessage, nameof(RegisterUser)),
                    code: HttpStatusCode.InternalServerError
                ));
        }

        [AllowAnonymous]
        [HttpGet($"{nameof(ForgotPassword)}/{{emailId}}")]
        public async Task<IActionResult> ForgotPassword(string emailId)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = _modelValidatorService.ValidateModelState(ModelState);
                return Ok(errorMessage);
            }

            var result = await _forgotPasswordServicesWordService.GetForgotPassword(emailId);
            return Ok(string.IsNullOrEmpty(result)
                ? _aPiResponseFactoryService.CreateResponse(data: result, success: false,
                    code:HttpStatusCode.NotFound,
                    statusMessage: CommonConstants.StatusMessageNotFound, error:
                    [
                        new Error(errorName: string.Format(CommonConstants.NotFound, nameof(User)))
                    ])
                : _aPiResponseFactoryService.CreateResponse(data: result, success: true,
                    code:HttpStatusCode.OK,
                    statusMessage: CommonConstants.StatusMessageOk));
        }
    }
}