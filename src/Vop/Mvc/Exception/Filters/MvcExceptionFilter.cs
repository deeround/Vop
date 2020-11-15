using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using Vop.Api.FluentException;

namespace Vop.Api
{
    public class MvcExceptionFilter : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            await Task.Run(() =>
            {
                // 标识异常已经被处理
                context.ExceptionHandled = true;

                // 设置异常结果
                var exception = context.Exception;

                //var output = new Output()
                //{
                //    Code = 0,
                //    Msg = "操作失败",
                //    ErrMsg = exception.Message
                //};
                //var result = new ObjectResult(output);
                //context.Result = result;

                if (exception != null)
                {
                    if (exception is ApiException) throw exception;
                    else throw new ApiException(exception.Message, exception);
                }
            });
        }
    }
}