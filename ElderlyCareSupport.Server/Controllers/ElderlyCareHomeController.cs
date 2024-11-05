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
    public class ElderlyCareHomeController : ControllerBase
    {
        private readonly IFeeRepository repository;
        private readonly ILogger<ElderlyCareHomeController> logger;
        private readonly ILoginRepository loginRepository;
        public ElderlyCareHomeController(IFeeRepository feeRepository, ILogger<ElderlyCareHomeController> logger, ILoginRepository loginRepository)
        {
            this.repository = feeRepository;
            this.logger = logger;
            this.loginRepository = loginRepository;
        }

        [AllowAnonymous]
        [HttpGet("GetFeeDetails")]
        public ActionResult GetFeeDetails()
        {
            var feeDetails = repository.GetAllFeeDetails();
            if (feeDetails.Result.Count >= 1)
            {
                logger.LogInformation($"Data Successfully fetched from the server...\nClass: {nameof(ElderlyCareHomeController)} Method: {nameof(GetFeeDetails)}");
                return Ok(feeDetails);
            }
            else
            {
                logger.LogInformation($"Data Successfully fetched from the server...\nClass: {nameof(ElderlyCareHomeController)} Method: {nameof(GetFeeDetails)}");
                return Ok(feeDetails);
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult Login([FromQuery] LoginViewModel loginViewModel)
        {
            var result = loginRepository.AuthenticateLogin(loginViewModel);
            if(result.Result)
            {
                return Ok(result);
            }
            else
            {
                return Ok(result);
            }
        }

    }
}
