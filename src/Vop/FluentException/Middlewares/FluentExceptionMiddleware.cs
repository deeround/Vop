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
        private readonly IFluentExceptionProvider _fluentExceptionProvider;

        public FluentExceptionMiddleware(RequestDelegate next, IFluentExceptionProvider fluentExceptionProvider)
        {
            _next = next;
            _fluentExceptionProvider = fluentExceptionProvider;
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
            await response.WriteAsync(JsonHelper.Serialize(_fluentExceptionProvider.OnException(exception))).ConfigureAwait(false);
        }



    }
}