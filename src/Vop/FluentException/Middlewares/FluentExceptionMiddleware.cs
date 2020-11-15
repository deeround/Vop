using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Vop.Api.FluentException
{
    public class FluentExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public FluentExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception == null) return;

            ApiException exp = ResolveException(exception);
            Output output = new Output()
            {
                Code = 0,
                Msg = exp.Msg,
                ErrCode = exp.Code,
                ErrMsg = exp.Message,
            };

            var response = context.Response;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.ContentType = "application/json";

            await response.WriteAsync(JsonHelper.Serialize(output)).ConfigureAwait(false);
        }

        private ApiException ResolveException(Exception exception)
        {
            if (exception is ApiException) return exception as ApiException;
            else if (exception.InnerException != null && exception.InnerException is ApiException) return exception.InnerException as ApiException;
            else return new ApiUnknowException(exception.Message, exception);
        }

    }
}