using ElderlyCareSupport.Server.Common;
using ElderlyCareSupport.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Refit;

namespace ElderlyCareSupport.Server.Helpers
{
    public class ModelValidatorHelper(IAPIResponseFactoryService aPIResponseFactoryService) : IModelValidatorService
    {
        public APIResponseModel<IEnumerable<string>> ValidateModelState(ModelStateDictionary modelStateDictionary)
        {
            var errorList = modelStateDictionary
                            .Where(m => m.Value!.Errors.Any())
                            .SelectMany(m => m.Value!.Errors)
                            .Select(e => new Error { ErrorName = e.ErrorMessage})
                            .ToArray();

            return aPIResponseFactoryService.CreateResponse(
                data: (IEnumerable<string>)[],
                success: false,
                statusMessage: CommonConstants.STATUS_MESSAGE_BAD_REQUEST,
                errorMessage: CommonConstants.VALIDATION_ERROR_MESSAGE,
                error: errorList
            );
        }

    }
}
