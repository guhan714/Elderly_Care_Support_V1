using System.Net;
using ElderlyCareSupport.Application.Common;
using ElderlyCareSupport.Application.Contracts.Response;
using ElderlyCareSupport.Application.IService;
using Microsoft.AspNetCore.Mvc;

namespace ElderlyCareSupport.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ElderlyCareSupportHomeController : ControllerBase
    {
        private readonly IFeeService _feeService;
        private readonly ILogger<ElderlyCareSupportHomeController> _logger;
        private readonly IApiResponseFactoryService _aPiResponseFactoryService;

        public ElderlyCareSupportHomeController(IFeeService feeService,
            ILogger<ElderlyCareSupportHomeController> logger,
            IApiResponseFactoryService aPiResponseFactoryService)
        {
            _feeService = feeService;
            _logger = logger;
            _aPiResponseFactoryService = aPiResponseFactoryService;
        }


        [HttpGet(nameof(GetFeeDetails))]
        public async Task<IActionResult> GetFeeDetails()
        {
            var feeConfigurationDto = await _feeService.GetAllFeeDetails();
            if (feeConfigurationDto.Count == 0)
            {
                _logger.LogInformation(
                    $"Data Couldn't be fetched from the server...\nClass: {nameof(ElderlyCareSupportHomeController)} Method: {nameof(GetFeeDetails)}");
                return Ok(_aPiResponseFactoryService.CreateResponse(success: false,
                    statusMessage: Constants.StatusMessageNotFound, data: feeConfigurationDto,
                    code: HttpStatusCode.NotFound,
                    errorMessage: string.Format(Constants.NotFound, "Fee Details")));
            }

            _logger.LogInformation(
                $"Data Successfully fetched from the server...\nClass: {nameof(ElderlyCareSupportHomeController)} Method: {nameof(GetFeeDetails)}");
            return Ok(_aPiResponseFactoryService.CreateResponse(success: true,
                statusMessage: Constants.StatusMessageOk, code: HttpStatusCode.OK,
                data: feeConfigurationDto));
        }
    }
}