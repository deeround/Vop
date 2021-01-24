using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using Vop.Api.FluentException;

namespace Vop.Api.Mvc
{
    public class MvcExceptionFilter : IAsyncExceptionFilter
    {
        private readonly IMvcExceptionProvider _mvcExceptionProvider;

        public MvcExceptionFilter(IMvcExceptionProvider mvcExceptionProvider)
        {
            _mvcExceptionProvider = mvcExceptionProvider;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            await Task.Run(() =>
            {
                if (context.Exception != null)
                {
                    // 标识异常已经被处理
                    context.ExceptionHandled = true;

                    _mvcExceptionProvider.OnException(context);
                }
            });
        }
    }
}