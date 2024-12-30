using System.Net;
using ElderlyCareSupport.Server.Common;
using ElderlyCareSupport.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ElderlyCareSupport.Server.Helpers
{
    public class ModelValidatorHelper(IApiResponseFactoryService apiResponseFactoryService) : IModelValidatorService
    {
        public ApiResponseModel<IEnumerable<string>> ValidateModelState(ModelStateDictionary modelStateDictionary)
        {
            var errorList = modelStateDictionary
                .Where(m => m.Value!.Errors.Any())
                .SelectMany(m => m.Value!.Errors)
                .Select(e => new Error { ErrorName = e.ErrorMessage })
                .ToArray();

            return apiResponseFactoryService.CreateResponse(
                data: Enumerable.Empty<string>(),
                success: false,
                code:HttpStatusCode.BadRequest,
                statusMessage: CommonConstants.StatusMessageBadRequest,
                errorMessage: CommonConstants.ValidationErrorMessage,
                error: errorList
            );
        }
    }
}