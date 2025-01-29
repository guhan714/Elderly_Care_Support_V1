using ElderlyCareSupport.Application.Contracts.Response;
using FluentValidation.Results;

namespace ElderlyCareSupport.Application.IService
{
    public interface IModelValidatorService
    {
        ApiResponseModel<IEnumerable<string>> ValidateModelState(IEnumerable<ValidationFailure> validationFailures);
    }
}
