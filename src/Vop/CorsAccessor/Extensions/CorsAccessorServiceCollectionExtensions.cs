using Microsoft.AspNetCore.Builder;
using System;
using Vop.Api.CorsAccessor;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CorsAccessorServiceCollectionExtensions
    {
        public static IServiceCollection AddCorsAccessor(this IServiceCollection services, CorsAccessorBuilderOptions option)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(option.Name, option.Build());
            });

            return services;
        }

        public static IApplicationBuilder UseCorsAccessor(this IApplicationBuilder app, string policyName)
        {
            app.UseCors(policyName);

            return app;
        }
    }
}