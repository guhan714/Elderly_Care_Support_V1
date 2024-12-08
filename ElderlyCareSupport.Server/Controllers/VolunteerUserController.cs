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
        [HttpGet("GetVolunteerDetails/{emailId}")]
        public async Task<IActionResult> GetVolunteertUserDetailsById(string emailId)
        {
            var volunteerUser = await userProfileService.GetUserDetails(emailId);
            return Ok(responseFactoryService.CreateResponse(data: volunteerUser, success: true, statusMessage: CommonConstants.StatusMessageOk));
        }

        [HttpPut("UpdateVolunteerUserDetails/{emailId}")]
        [Authorize]
        public async Task<IActionResult> UpdateElderDetails(string emailId, [FromBody] VolunteerUserDto? elderCareAccount)
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
