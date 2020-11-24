using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using Vop.Api.FluentResult;

namespace Vop.Api.Mvc
{
    public class MvcResultFilter : IAsyncActionFilter, IOrderedFilter
    {
        private readonly IMvcResultProvider _mvcResultProvider;

        public MvcResultFilter(IMvcResultProvider mvcResultProvider)
        {
            _mvcResultProvider = mvcResultProvider;
        }

        public int Order => 9999;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var actionExecutedContext = await next();

            if (actionExecutedContext.Exception == null)
            {
                _mvcResultProvider.OnSuccessed(actionExecutedContext);
            }
        }
    }
}