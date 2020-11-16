using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using Vop.Api.FluentResult;

namespace Vop.Api.FluentException
{
    public class FluentExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IFluentResultProvider _fluentResultProvider;

        public FluentExceptionMiddleware(RequestDelegate next, IFluentResultProvider fluentResultProvider)
        {
            _next = next;
            _fluentResultProvider = fluentResultProvider;
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

            var response = context.Response;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.ContentType = "application/json";
            await response.WriteAsync(JsonHelper.Serialize(_fluentResultProvider.OnException(exception))).ConfigureAwait(false);
        }



    }
}