using ElderlyCareSupport.Server.Common;
using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Helpers;
using ElderlyCareSupport.Server.Models;
using ElderlyCareSupport.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Cmp;

namespace ElderlyCareSupport.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ElderlyUserController(IUserProfileService<ElderUserDTO> elderlyUserProfileService, IAPIResponseFactoryService aPIResponseFactoryService, IModelValidatorService modelValidatorService) : ControllerBase
    {
        [HttpGet("GetElderlyUserDetails/{emailID}")]
        [Authorize]
        public async Task<IActionResult> GetElderlyUsersList(string emailID)
        {
            var elderlyUser = await elderlyUserProfileService.GetUserDetails(emailID);

            return Ok(aPIResponseFactoryService.CreateResponse(success: true, statusMessage: CommonConstants.STATUS_MESSAGE_OK, data: elderlyUser));
        }

        [HttpPut("UpdateElderlyUserDetails/{emailID}")]
        [Authorize]
        public async Task<IActionResult> UpdateElderDetails(string emailID,[FromBody] ElderUserDTO elderCareAccount)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = modelValidatorService.ValidateModelState(ModelState);
                return (Ok(errorMessage));
            }

            var updateResult = await elderlyUserProfileService.UpdateUserDetails(emailID,elderCareAccount);

            return Ok(aPIResponseFactoryService.CreateResponse(success: true, statusMessage: CommonConstants.STATUS_MESSAGE_OK, data: new List<string>()));
        }


    }
}
