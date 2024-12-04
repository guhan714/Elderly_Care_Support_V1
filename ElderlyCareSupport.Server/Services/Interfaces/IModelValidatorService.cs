using ElderlyCareSupport.Server.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ElderlyCareSupport.Server.Services.Interfaces
{
    public interface IModelValidatorService
    {
        APIResponseModel<IEnumerable<string>> ValidateModelState(ModelStateDictionary modelStateDictionary);
    }
}
