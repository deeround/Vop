using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vop.Api.FluentException;

namespace Vop.Api.Mvc
{
    public class DefaultMvcValidationProvider : IMvcValidationProvider
    {
        public virtual void OnValidate(ModelStateDictionary modelState)
        {
            throw new ApiValidationException("Your request is not valid, please correct and try again!");
        }
    }
}