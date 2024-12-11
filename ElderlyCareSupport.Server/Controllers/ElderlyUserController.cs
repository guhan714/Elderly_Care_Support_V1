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
    public class ElderlyUserController(IUserProfileService<ElderUserDto> elderlyUserProfileService, IApiResponseFactoryService aPiResponseFactoryService, IModelValidatorService modelValidatorService) : ControllerBase
    {
        [HttpGet($"{nameof(GetElderlyUserDetails)}/{{emailId}}")]
        public async Task<IActionResult> GetElderlyUserDetails(string emailId)
        {
            var elderlyUser = await elderlyUserProfileService.GetUserDetails(emailId);
            return elderlyUser is null ? Ok(aPiResponseFactoryService.CreateResponse(success: false, statusMessage: CommonConstants.StatusMessageNotFound, data: elderlyUser)) : Ok(aPiResponseFactoryService.CreateResponse(success: true, statusMessage: CommonConstants.StatusMessageOk, data: elderlyUser));
        }

        [HttpPut($"{nameof(UpdateElderDetails)}/{{emailId}}")]
        public async Task<IActionResult> UpdateElderDetails(string emailId,[FromBody] ElderUserDto? elderCareAccount)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = modelValidatorService.ValidateModelState(ModelState);
                return Ok(errorMessage);
            }

            var updateResult = await elderlyUserProfileService.UpdateUserDetails(emailId,elderCareAccount);

            return Ok(aPiResponseFactoryService.CreateResponse(success: updateResult, statusMessage: CommonConstants.StatusMessageOk, data: new List<string>()));
        }


    }
}
