using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using Vop.Api.FluentResult;

namespace Vop.Api
{
    public class MvcResultFilter : IAsyncActionFilter, IOrderedFilter
    {
        private readonly IFluentResultProvider _fluentResultProvider;

        public MvcResultFilter(IFluentResultProvider fluentResultProvider)
        {
            _fluentResultProvider = fluentResultProvider;
        }

        public int Order => 9999;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var actionExecutedContext = await next();

            if (actionExecutedContext.Exception == null)
            {
                object data;
                if (actionExecutedContext.Result is ContentResult contentResult) data = contentResult.Content;
                else if (actionExecutedContext.Result is ObjectResult objectResult) data = objectResult.Value;
                else data = null;
                actionExecutedContext.Result = _fluentResultProvider.OnSuccessed(data);
            }
        }
    }
}