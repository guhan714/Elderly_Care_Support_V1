using ElderlyCareSupport.Server.HelperInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElderlyCareSupport.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElderlyCareHomeController : ControllerBase
    {
        private readonly IFeeRepository repository;

        public ElderlyCareHomeController(IFeeRepository feeRepository)
        {
            this.repository = feeRepository;
        }

        [HttpGet]
        public ActionResult GetFeeDetails()
        {
            var feeDetails = repository.GetAllFeeDetails();
            return Ok(feeDetails);
        }
    }
}
