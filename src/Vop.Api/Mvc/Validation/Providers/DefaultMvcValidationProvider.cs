using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using Vop.Api.FluentException;

namespace Vop.Api.Mvc
{
    public class DefaultMvcValidationProvider : IMvcValidationProvider
    {
        public virtual void OnValidate(ModelStateDictionary modelState)
        {
            var ex = new ApiValidationException("Your request is not valid, please correct and try again!");
            foreach (var state in modelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    ex.ValidationErrors.Add(new ValidationResult(error.ErrorMessage, new[] { state.Key }));
                }
            }
            throw ex;
        }
    }
}