using ElderlyCareSupport.Server.Common;
using ElderlyCareSupport.Server.DataRepository;
using ElderlyCareSupport.Server.HelperInterface;
using ElderlyCareSupport.Server.Interfaces;
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
        private readonly IFeeRepository repository;
        private readonly ILogger<ElderlyCareHomeController> logger;
        private readonly ILoginRepository loginRepository;
        private readonly IRegistrationRepository registrationRepository;
        public ElderlyCareHomeController(IFeeRepository feeRepository, ILogger<ElderlyCareHomeController> logger, ILoginRepository loginRepository, IRegistrationRepository registrationRepository)
        {
            this.repository = feeRepository;
            this.logger = logger;
            this.loginRepository = loginRepository;
            this.registrationRepository = registrationRepository;
        }

        [AllowAnonymous]
        [HttpGet("GetFeeDetails")]
        public ActionResult GetFeeDetails()
        {
            var feeDetails = repository.GetAllFeeDetails();
            if (feeDetails.Result.Count >= 1)
            {
                logger.LogInformation($"Data Successfully fetched from the server...\nClass: {nameof(ElderlyCareHomeController)} Method: {nameof(GetFeeDetails)}");
                return Ok(APIResponseFactory.CreateResponse(success:true,statusMessage: "Ok", data: feeDetails.Result));
            }
            else
            {
                logger.LogInformation($"Data Couldn't be fetched from the server...\nClass: {nameof(ElderlyCareHomeController)} Method: {nameof(GetFeeDetails)}");
                return Ok(APIResponseFactory.CreateResponse(success:false, statusMessage: "NotFound", data: feeDetails.Result, errorMessage: "Can't Find the Fee Details"));
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType<APIResponseModel<object>>(StatusCodes.Status200OK)]
        public ActionResult Login([FromQuery] LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = loginRepository.AuthenticateLogin(loginViewModel);
                if (result.Result)
                {
                    return Ok(APIResponseFactory.CreateResponse(data: result.Result.ToString(), success: true, statusMessage: "Ok"));
                }
                return Ok(APIResponseFactory.CreateResponse(data: result.Result.ToString(), success: false, statusMessage: "NotFound",errorMessage: "User is not found"));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public ActionResult RegisterUser([FromBody] RegistrationViewModel registerViewModel)
        {

            if (ModelState.IsValid)
            {
                var registrationStatus = registrationRepository.RegisterUser(registerViewModel);
                if (registrationStatus.Result)
                {
                    var registrationResponse = new APIResponseModel<object>
                    {
                        StatusCode = 200,
                        StatusMessage = "OK",
                        Data = registrationStatus.Result,
                        Success = true
                    };
                    return Ok(APIResponseFactory.CreateResponse(data: registrationStatus.Result.ToString(), success: true, statusMessage: "Ok"));
                }
                else
                    return Ok(APIResponseFactory.CreateResponse(data: registrationStatus.Result.ToString(), success: true, statusMessage: "Ok"));
            }

            return BadRequest(ModelState);
        }

    }
}
