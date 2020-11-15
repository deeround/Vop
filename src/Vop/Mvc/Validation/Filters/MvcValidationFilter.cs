using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using Vop.Api.FluentException;

namespace Vop.Api
{
    public class MvcValidationFilter : IAsyncActionFilter, IOrderedFilter
    {
        public int Order => -9999;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var modelState = context.ModelState;
            if (!modelState.IsValid)
            {
                //var output = new Output()
                //{
                //    Code = 0,
                //    Msg = "操作失败",
                //    ErrMsg = "验证不通过"
                //};
                //var result = new ObjectResult(output);
                //context.Result = result;

                throw new ApiValidationException("Your request is not valid, please correct and try again!");
            }
            else
            {
                await next();
            }
        }
    }
}