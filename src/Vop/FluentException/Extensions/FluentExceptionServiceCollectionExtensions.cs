﻿using Microsoft.AspNetCore.Builder;
using System;
using Vop.Api.FluentException;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FluentExceptionServiceCollectionExtensions
    {
        public static IServiceCollection AddFluentException(this IServiceCollection services, ErrorCodeOptions option)
        {
            return services;
        }

        public static IApplicationBuilder UseFluentException(this IApplicationBuilder app)
        {
            //异常处理中间件
            app.UseMiddleware(typeof(FluentExceptionMiddleware));

            return app;
        }
    }
}