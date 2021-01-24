using Microsoft.AspNetCore.Builder;
using System;
using Vop.Api.FluentException;
using Vop.Api.FluentResult;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FluentResultServiceCollectionExtensions
    {
        public static IServiceCollection AddFluentResult(this IServiceCollection services)
        {
            services.AddSingleton<IFluentResultProvider, DefaultFluentResultProvider>();

            return services;
        }

        public static IApplicationBuilder UseFluentResult(this IApplicationBuilder app)
        {
            return app;
        }
    }
}