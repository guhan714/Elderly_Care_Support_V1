using ElderlyCareSupport.Server.Common;
using ElderlyCareSupport.Server.Helpers;
using ElderlyCareSupport.Server.Services.Interfaces;
using ElderlyCareSupport.Server.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElderlyCareSupport.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ElderlyCareSupportAccountController(IFeeService feeService, ILogger<ElderlyCareSupportAccountController> logger, ILoginService loginService, IRegistrationService registrationService, IForgotPaswordService forgotPaswordService, ITokenService tokenService, IAPIResponseFactoryService aPIResponseFactoryService, IModelValidatorService modelValidatorService) : ControllerBase
    {
        [HttpGet("GetFeeDetails")]
        public async Task<IActionResult> GetFeeDetails()
        {
            var feeDetails = await feeService.GetAllFeeDetails();
            if (feeDetails.Any())
            {
                logger.LogInformation($"Data Successfully fetched from the server...\nClass: {nameof(ElderlyCareSupportAccountController)} Method: {nameof(GetFeeDetails)}");
                return Ok(aPIResponseFactoryService.CreateResponse(success:true,statusMessage: CommonConstants.STATUS_MESSAGE_OK, data: feeDetails));
            }
            else
            {
                logger.LogInformation($"Data Couldn't be fetched from the server...\nClass: {nameof(ElderlyCareSupportAccountController)} Method: {nameof(GetFeeDetails)}");
                return Ok(aPIResponseFactoryService.CreateResponse(success:false, statusMessage: CommonConstants.STATUS_MESSAGE_NOT_FOUND, data: feeDetails, errorMessage: String.Format(CommonConstants.NOT_FOUND, "Fee Details")));
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromQuery] LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = modelValidatorService.ValidateModelState(ModelState);
                return(Ok(errorMessage));
            }
            var result = await loginService.AuthenticateLogin(loginViewModel);
            if (result)
            {
                var token = tokenService.GenerateJWTToken(loginViewModel.Email);
                if(!String.IsNullOrEmpty(token))
                {
                    return Ok(aPIResponseFactoryService.CreateResponse(data: token, success: true, statusMessage: CommonConstants.STATUS_MESSAGE_OK));
                }
                return Ok(aPIResponseFactoryService.CreateResponse(data: token, success: false, statusMessage: CommonConstants.STATUS_MESSAGE_OK, errorMessage: "Can't get the token from the server."));
            }
            return Unauthorized(aPIResponseFactoryService.CreateResponse(data: result.ToString(), success: false, statusMessage: CommonConstants.STATUS_MESSAGE_NOT_FOUND, errorMessage: String.Format(CommonConstants.NOT_FOUND, "User")));
        }

        [AllowAnonymous]
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] RegistrationViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = modelValidatorService.ValidateModelState(ModelState);
                return (Ok(errorMessage));
            }

            var result = registrationService.checkUserExistingAlready(registerViewModel.Email);
            if (result.Result)
            {
                return Ok(aPIResponseFactoryService.CreateResponse(data: Array.Empty<string>(), success: false, statusMessage: CommonConstants.USER_ALREADY_EXISTED, error: [new Error { ErrorName = CommonConstants.USER_ALREADY_EXISTED }]));
            }

            var registrationStatus = await registrationService.RegisterUserAsync(registerViewModel);

            if (registrationStatus)
            {
                return Ok(aPIResponseFactoryService.CreateResponse(data: registrationStatus.ToString(), success: true, statusMessage: CommonConstants.STATUS_MESSAGE_OK));
            }
            return Ok(aPIResponseFactoryService.CreateResponse(data: registrationStatus.ToString(), success: false, statusMessage: String.Format(CommonConstants.OPERATION_FAILED_ERROR_MESSAGE_), nameof(RegisterUser)));
        }

        [AllowAnonymous]
        [HttpGet("ForgotPassword/{userName}")]
        public async Task<IActionResult> ForgotPassword(string userName)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = modelValidatorService.ValidateModelState(ModelState);
                return (Ok(errorMessage));
            }

            var result = await forgotPaswordService.GetForgotPassword(userName);
            if (String.IsNullOrEmpty(result))
            {
                return Ok(aPIResponseFactoryService.CreateResponse(data: result, success: false, statusMessage: CommonConstants.STATUS_MESSAGE_NOT_FOUND, error: [new Error { ErrorName = String.Format(CommonConstants.NOT_FOUND, "User") }]));
            }
            return Ok(aPIResponseFactoryService.CreateResponse(data: result, success: true, statusMessage: CommonConstants.STATUS_MESSAGE_OK));
        }

    }
    

}
