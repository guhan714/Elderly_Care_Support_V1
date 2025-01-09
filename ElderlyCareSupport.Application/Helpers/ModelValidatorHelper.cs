using System.Net;
using System.Web.Http.ModelBinding;
using ElderlyCareSupport.Application.Common;
using ElderlyCareSupport.Application.Contracts;
using ElderlyCareSupport.Application.Contracts.Common;
using ElderlyCareSupport.Application.IService;
using FluentValidation.Results;

namespace ElderlyCareSupport.Application.Helpers
{
    public class ModelValidatorHelper(IApiResponseFactoryService apiResponseFactoryService) : IModelValidatorService
    {
        public ApiResponseModel<IEnumerable<string>> ValidateModelState(List<ValidationFailure> modelStateDictionary)
        {
            var errorList = modelStateDictionary
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