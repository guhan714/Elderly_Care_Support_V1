using ElderlyCareSupport.Server.Common;
using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElderlyCareSupport.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VolunteerUserController(IUserProfileService<VolunteerUserDto> userProfileService, IApiResponseFactoryService responseFactoryService, IModelValidatorService modelValidatorService) : ControllerBase
    {
        [HttpGet($"{nameof(GetVolunteerUserDetailsById)}/{{emailId}}")]
        public async Task<IActionResult> GetVolunteerUserDetailsById(string emailId)
        {
            var volunteerUser = await userProfileService.GetUserDetails(emailId);
            return volunteerUser is not null ? Ok(responseFactoryService.CreateResponse(data: volunteerUser, success: true, statusMessage: CommonConstants.StatusMessageOk)) : Ok(responseFactoryService.CreateResponse(data: volunteerUser, success: false, statusMessage: CommonConstants.StatusMessageNotFound, error: [new Error(){ErrorName = string.Format(CommonConstants.NotFound, nameof(User))}]));
        }

        [HttpPut($"{nameof(UpdateElderDetailsById)}/{{emailId}}")]
        [Authorize]
        public async Task<IActionResult> UpdateElderDetailsById(string emailId, [FromBody] VolunteerUserDto? elderCareAccount)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = modelValidatorService.ValidateModelState(ModelState);
                return Ok(errorMessage);
            }

            var updateResult = await userProfileService.UpdateUserDetails(emailId, elderCareAccount);

            return Ok(responseFactoryService.CreateResponse(success: updateResult, statusMessage: CommonConstants.StatusMessageOk, data: new List<string>()));
        }

    }
}
