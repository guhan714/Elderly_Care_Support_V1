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
    public class ElderlyCareSupportAccountController(IFeeService feeService, ILogger<ElderlyCareSupportAccountController> logger, ILoginService loginService, IRegistrationService registrationService, IForgotPaswordService forgotPasswordServicesWordService, ITokenService tokenService, IApiResponseFactoryService aPiResponseFactoryService, IModelValidatorService modelValidatorService) : ControllerBase
    {
        [HttpGet("GetFeeDetails")]
        public async Task<IActionResult> GetFeeDetails()
        {
            var feeConfigurationDto = await feeService.GetAllFeeDetails();
            if (feeConfigurationDto.Count != 0)
            {
                logger.LogInformation($"Data Successfully fetched from the server...\nClass: {nameof(ElderlyCareSupportAccountController)} Method: {nameof(GetFeeDetails)}");
                return Ok(aPiResponseFactoryService.CreateResponse(success:true,statusMessage: CommonConstants.StatusMessageOk, data: feeConfigurationDto));
            }
            logger.LogInformation($"Data Couldn't be fetched from the server...\nClass: {nameof(ElderlyCareSupportAccountController)} Method: {nameof(GetFeeDetails)}");
            return Ok(aPiResponseFactoryService.CreateResponse(success:false, statusMessage: CommonConstants.StatusMessageNotFound, data: feeConfigurationDto, errorMessage: string.Format(CommonConstants.NotFound, "Fee Details")));
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromQuery] LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = modelValidatorService.ValidateModelState(ModelState);
                return Ok(errorMessage);
            }
            var result = await loginService.AuthenticateLogin(loginViewModel);
            if (result)
            {
                var token = tokenService.GenerateJwtToken(loginViewModel.Email);
                return Ok(!string.IsNullOrEmpty(token)
                    ? aPiResponseFactoryService.CreateResponse(data: token, success: true,
                        statusMessage: CommonConstants.StatusMessageOk)
                    : aPiResponseFactoryService.CreateResponse(data: token, success: false,
                        statusMessage: CommonConstants.StatusMessageBadRequest,
                        errorMessage: "Can't get the token from the server."));
            }

            return Unauthorized(aPiResponseFactoryService.CreateResponse(data: result.ToString(), success: false,
                statusMessage: CommonConstants.StatusMessageNotFound,
                errorMessage: string.Format(CommonConstants.NotFound, "User")));

        }

        [AllowAnonymous]
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] RegistrationViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = modelValidatorService.ValidateModelState(ModelState);
                return Ok(errorMessage);
            }

            var result = registrationService.CheckUserExistingAlready(registerViewModel.Email);
            if (result.Result)
            {
                return Ok(aPiResponseFactoryService.CreateResponse(data: Array.Empty<string>(), success: false, statusMessage: CommonConstants.UserAlreadyExisted, error: [new Error { ErrorName = CommonConstants.UserAlreadyExisted }]));
            }

            var registrationStatus = await registrationService.RegisterUserAsync(registerViewModel);

            return Ok(registrationStatus ? aPiResponseFactoryService.CreateResponse(data: registrationStatus.ToString(), success: true, statusMessage: CommonConstants.StatusMessageOk) : aPiResponseFactoryService.CreateResponse(data: registrationStatus.ToString(), success: false, statusMessage: string.Format(CommonConstants.OperationFailedErrorMessage, nameof(RegisterUser)), nameof(RegisterUser)));
        }

        [AllowAnonymous]
        [HttpGet("ForgotPassword/{userName}")]
        public async Task<IActionResult> ForgotPassword(string userName)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = modelValidatorService.ValidateModelState(ModelState);
                return Ok(errorMessage);
            }

            var result = await forgotPasswordServicesWordService.GetForgotPassword(userName);
            return Ok(string.IsNullOrEmpty(result) ? aPiResponseFactoryService.CreateResponse(data: result, success: false, statusMessage: CommonConstants.StatusMessageNotFound, error: [
                new Error(errorName: string.Format(CommonConstants.NotFound, "User"))
            ]) : aPiResponseFactoryService.CreateResponse(data: result, success: true, statusMessage: CommonConstants.StatusMessageOk));
        }

    }
    

}
