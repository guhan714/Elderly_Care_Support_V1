using System.Net;
using ElderlyCareSupport.Application.Common;
using ElderlyCareSupport.Application.Contracts.Response;
using ElderlyCareSupport.Application.DTOs;
using ElderlyCareSupport.Application.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElderlyCareSupport.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VolunteerUserController(
        IUserProfileService<VolunteerUserDto> userProfileService,
        IApiResponseFactoryService responseFactoryService,
        IModelValidatorService modelValidatorService) : ControllerBase
    {
        [HttpGet($"{nameof(GetVolunteerUserDetailsById)}/{{emailId}}")]
        public async Task<IActionResult> GetVolunteerUserDetailsById(string emailId)
        {
            var volunteerUser = await userProfileService.GetUserDetails(emailId);
            return volunteerUser is not null
                ? Ok(responseFactoryService.CreateResponse(data: volunteerUser, success: true, code: HttpStatusCode.OK
                    , statusMessage: Constants.StatusMessageOk))
                : Ok(responseFactoryService.CreateResponse(data: volunteerUser, success: false,
                    code: HttpStatusCode.NotFound
                    , statusMessage: Constants.StatusMessageNotFound,
                    error: [new Error { ErrorName = string.Format(Constants.NotFound, nameof(User)) }]));
        }

        [HttpPut($"{nameof(UpdateElderDetailsById)}/{{emailId}}")]
        public async Task<IActionResult> UpdateElderDetailsById(string emailId,
            [FromBody] VolunteerUserDto? elderCareAccount)
        {
            // if (!ModelState.IsValid)
            // {
            //     var errorMessage = modelValidatorService.ValidateModelState(ModelState);
            //     return Ok(errorMessage);
            // }

            var updateResult = await userProfileService.UpdateUserDetails(emailId, elderCareAccount);

            return Ok(responseFactoryService.CreateResponse(success: updateResult,
                code: updateResult ? HttpStatusCode.OK : HttpStatusCode.InternalServerError,
            statusMessage:
            Constants.StatusMessageOk, data:
            new List<string>()));
        }
    }
}