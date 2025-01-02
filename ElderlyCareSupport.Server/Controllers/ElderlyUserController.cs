using System.Net;
using ElderlyCareSupport.Server.Common;
using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElderlyCareSupport.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize]
    public class ElderlyUserController : ControllerBase
    {
        private readonly IUserProfileService<ElderUserDto> _elderlyUserProfileService;
        private readonly IApiResponseFactoryService _aPiResponseFactoryService;
        private readonly IModelValidatorService _modelValidatorService;

        public ElderlyUserController(IUserProfileService<ElderUserDto> elderlyUserProfileService,
            IApiResponseFactoryService aPiResponseFactoryService, IModelValidatorService modelValidatorService)
        {
            _elderlyUserProfileService = elderlyUserProfileService;
            _aPiResponseFactoryService = aPiResponseFactoryService;
            _modelValidatorService = modelValidatorService;
        }

        [HttpGet($"{nameof(GetElderlyUserDetails)}/{{emailId}}")]
        public async Task<IActionResult> GetElderlyUserDetails(string emailId)
        {
            var elderlyUser = await _elderlyUserProfileService.GetUserDetails(emailId);
            return elderlyUser is null
                ? Ok(_aPiResponseFactoryService.CreateResponse(success: false,
                    code: HttpStatusCode.NoContent,
                    statusMessage: CommonConstants.StatusMessageNotFound, data: elderlyUser,
                    errorMessage: string.Format(CommonConstants.NotFound, "user")))
                : Ok(_aPiResponseFactoryService.CreateResponse(success: true,
                    code: HttpStatusCode.OK,
                    statusMessage: CommonConstants.StatusMessageOk, data: elderlyUser));
        }

        [HttpPut($"{nameof(UpdateElderDetails)}/{{emailId}}")]
        public async Task<IActionResult> UpdateElderDetails(string emailId, [FromBody] ElderUserDto? elderCareAccount)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = _modelValidatorService.ValidateModelState(ModelState);
                return Ok(errorMessage);
            }

            var updateResult = await _elderlyUserProfileService.UpdateUserDetails(emailId, elderCareAccount);

            return Ok(_aPiResponseFactoryService.CreateResponse(success: updateResult,
                code: HttpStatusCode.Created,
                statusMessage: CommonConstants.StatusMessageOk, data: new List<string>()));
        }

        [HttpPost($"{nameof(CreateTask)}")]
        public async Task<IActionResult> CreateTask()
        {
            if (!ModelState.IsValid)
            {
                return Ok(_modelValidatorService.ValidateModelState(ModelState));
            }

            return Ok();
        }
    }
}