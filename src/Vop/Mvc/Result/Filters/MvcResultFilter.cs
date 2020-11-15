using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Vop.Api
{
    public class MvcResultFilter : IAsyncActionFilter, IOrderedFilter
    {
        public MvcResultFilter()
        {
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
                var output = new Output()
                {
                    Code = 1,
                    Msg = "操作成功",
                    Data = data
                };
                var result = new ObjectResult(output);
                actionExecutedContext.Result = result;
            }
        }
    }
}