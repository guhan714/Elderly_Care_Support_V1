using ElderlyCareSupport.Server.Common;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using ElderlyCareSupport.Server.Services.Interfaces;
using ElderlyCareSupport.Server.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElderlyCareSupport.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ElderlyCareHomeController : ControllerBase
    {
        private readonly IFeeService feeService;
        private readonly ILogger<ElderlyCareHomeController> logger;
        private readonly ILoginService loginService;
        private readonly IRegistrationService registrationService;
        private readonly IForgotPaswordService forgotPaswordService;
        public ElderlyCareHomeController(IFeeService feeService, ILogger<ElderlyCareHomeController> logger, ILoginService loginService, IRegistrationService registrationService, IForgotPaswordService forgotPaswordService)
        {
            this.feeService = feeService;
            this.logger = logger;
            this.loginService = loginService;
            this.registrationService = registrationService;
            this.forgotPaswordService = forgotPaswordService;
        }

        [AllowAnonymous]
        [HttpGet("GetFeeDetails")]
        public async Task<ActionResult> GetFeeDetails()
        {
            var feeDetails = await feeService.GetAllFeeDetails();
            if (feeDetails.Count >= 1)
            {
                logger.LogInformation($"Data Successfully fetched from the server...\nClass: {nameof(ElderlyCareHomeController)} Method: {nameof(GetFeeDetails)}");
                return Ok(APIResponseFactory.CreateResponse(success:true,statusMessage: "Ok", data: feeDetails));
            }
            else
            {
                logger.LogInformation($"Data Couldn't be fetched from the server...\nClass: {nameof(ElderlyCareHomeController)} Method: {nameof(GetFeeDetails)}");
                return Ok(APIResponseFactory.CreateResponse(success:false, statusMessage: "NotFound", data: feeDetails, errorMessage: "Can't Find the Fee Details"));
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromQuery] LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await loginService.AuthenticateLogin(loginViewModel);
                if (result)
                {
                    return Ok(APIResponseFactory.CreateResponse(data: result.ToString(), success: true, statusMessage: "Ok"));
                }
                return NotFound(APIResponseFactory.CreateResponse(data: result.ToString(), success: false, statusMessage: "NotFound",errorMessage: "User is not found"));
            }
            return BadRequest(ModelState);
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegistrationViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var registrationStatus = await registrationService.RegisterUserAsync(registerViewModel);
                if (registrationStatus)
                {
                    return Ok(APIResponseFactory.CreateResponse(data: registrationStatus.ToString(), success: true, statusMessage: "Ok"));
                }
                else
                    return Ok(APIResponseFactory.CreateResponse(data: registrationStatus.ToString(), success: true, statusMessage: "Ok"));
            }
            return BadRequest(ModelState);
        }

        [AllowAnonymous]
        [HttpGet("ForgotPassword")]
        public async Task<ActionResult> ForgotPassword([FromQuery]string userName)
        {
            if (ModelState.IsValid)
            {
                var result = await forgotPaswordService.GetForgotPassword(userName);
                if (String.IsNullOrEmpty(result))
                {
                    return Ok(APIResponseFactory.CreateResponse(data: result.ToString(), success: false, statusMessage: "NotFound"));
                }
                return Ok(APIResponseFactory.CreateResponse(data: result.ToString(), success: true, statusMessage: "Ok"));
            }
            return BadRequest(ModelState);
        }
    }
}
