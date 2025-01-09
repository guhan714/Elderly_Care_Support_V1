using System.Web.Http.ModelBinding;
using ElderlyCareSupport.Application.Contracts.Common;
using FluentValidation.Results;

namespace ElderlyCareSupport.Application.IService
{
    public interface IModelValidatorService
    {
        ApiResponseModel<IEnumerable<string>> ValidateModelState(List<ValidationFailure> validationFailures);
    }
}
