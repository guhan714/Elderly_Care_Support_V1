using System.Net;
using ElderlyCareSupport.Application.Common;
using ElderlyCareSupport.Application.Contracts.Response;
using ElderlyCareSupport.Application.IService;
using FluentValidation.Results;

namespace ElderlyCareSupport.Application.Helpers
{
    public class ModelValidatorHelper(IApiResponseFactoryService apiResponseFactoryService) : IModelValidatorService
    {
        public ApiResponseModel<IEnumerable<string>> ValidateModelState(IEnumerable<ValidationFailure> validationFailures)
        {
            var errorList = validationFailures
                .Select(e => new Error { ErrorName = e.ErrorMessage })
                .ToArray();

            return apiResponseFactoryService.CreateResponse(
                data: Enumerable.Empty<string>(),
                success: false,
                code:HttpStatusCode.BadRequest,
                statusMessage: Constants.StatusMessageBadRequest,
                errorMessage: Constants.ValidationErrorMessage,
                error: errorList
            );
        }
    }
}