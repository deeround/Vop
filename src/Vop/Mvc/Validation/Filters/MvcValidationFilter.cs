using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using Vop.Api.FluentException;

namespace Vop.Api.Mvc
{
    public class MvcValidationFilter : IAsyncActionFilter, IOrderedFilter
    {
        private readonly IMvcValidationProvider _mvcValidationProvider;

        public MvcValidationFilter(IMvcValidationProvider mvcValidationProvider)
        {
            _mvcValidationProvider = mvcValidationProvider;
        }

        public int Order => -9999;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var modelState = context.ModelState;
            if (!modelState.IsValid)
            {
                _mvcValidationProvider.OnValidate(modelState);
            }
            else
            {
                await next();
            }
        }
    }
}