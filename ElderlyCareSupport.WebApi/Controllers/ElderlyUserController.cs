using System.Net;
using ElderlyCareSupport.Application.Common;
using ElderlyCareSupport.Application.DTOs;
using ElderlyCareSupport.Application.IService;
using FluentValidation;
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
        private readonly IValidator<ElderUserDto> _validator;
        private readonly IValidator<string> _userNameValidator;

        public ElderlyUserController(IUserProfileService<ElderUserDto> elderlyUserProfileService,
            IApiResponseFactoryService aPiResponseFactoryService, IModelValidatorService modelValidatorService,
            IValidator<ElderUserDto> validator, IValidator<string> userNameValidator)
        {
            _elderlyUserProfileService = elderlyUserProfileService;
            _aPiResponseFactoryService = aPiResponseFactoryService;
            _modelValidatorService = modelValidatorService;
            _validator = validator;
            _userNameValidator = userNameValidator;
        }

        [HttpGet($"{nameof(GetElderlyUserDetails)}/{{emailId}}")]
        public async Task<IActionResult> GetElderlyUserDetails(string emailId)
        {
            var validationResult = await _userNameValidator.ValidateAsync(emailId);
            if(!validationResult.IsValid)
                return BadRequest(_modelValidatorService.ValidateModelState(validationResult.Errors));
            
            var elderlyUser = await _elderlyUserProfileService.GetUserDetails(emailId);
            return elderlyUser is null
                ? NotFound(_aPiResponseFactoryService.CreateResponse(success: false,
                    code: HttpStatusCode.NotFound,
                    statusMessage: CommonConstants.StatusMessageNotFound, data: elderlyUser,
                    errorMessage: string.Format(CommonConstants.NotFound, "user")))
                : Ok(_aPiResponseFactoryService.CreateResponse(success: true,
                    code: HttpStatusCode.OK,
                    statusMessage: CommonConstants.StatusMessageOk, data: elderlyUser));
        }

        [HttpPut($"{nameof(UpdateElderDetails)}/{{emailId}}")]
        public async Task<IActionResult> UpdateElderDetails(string emailId, [FromBody] ElderUserDto elderCareAccount)
        {
            var validationResult = await _validator.ValidateAsync(elderCareAccount);
            if (!validationResult.IsValid)
                return Ok(_modelValidatorService.ValidateModelState(validationResult.Errors));
            
            var updateResult = await _elderlyUserProfileService.UpdateUserDetails(emailId, elderCareAccount);

            return Ok(_aPiResponseFactoryService.CreateResponse(success: updateResult,
                code: updateResult ? HttpStatusCode.Created : HttpStatusCode.InternalServerError,
                statusMessage: CommonConstants.StatusMessageOk, data: new List<string>()));
        }

        [HttpPost($"{nameof(CreateTask)}")]
        public async Task<IActionResult> CreateTask()
        {
            if (!ModelState.IsValid)
            {
                return Ok();
            }

            return Ok();
        }
    }
}