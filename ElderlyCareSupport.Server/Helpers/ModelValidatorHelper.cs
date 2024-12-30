using ElderlyCareSupport.Server.Common;
using ElderlyCareSupport.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ElderlyCareSupport.Server.Helpers
{
    public class ModelValidatorHelper(IApiResponseFactoryService aPiResponseFactoryService) : IModelValidatorService
    {
        public ApiResponseModel<List<string>> ValidateModelState(ModelStateDictionary modelStateDictionary)
        {
            var errorList = modelStateDictionary
                            .Where(m => m.Value!.Errors.Any())
                            .SelectMany(m => m.Value!.Errors)
                            .Select(e => new Error { ErrorName = e.ErrorMessage})
                            .ToArray();

            return aPiResponseFactoryService.CreateResponse(
                data: Array.Empty<string>().ToList(),
                success: false,
                statusMessage: CommonConstants.StatusMessageBadRequest,
                errorMessage: CommonConstants.ValidationErrorMessage,
                error: errorList
            );
        }

    }
}
