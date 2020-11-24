using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Vop.Api.Mvc
{
    public interface IMvcValidationProvider
    {
        void OnValidate(ModelStateDictionary modelState);
    }
}