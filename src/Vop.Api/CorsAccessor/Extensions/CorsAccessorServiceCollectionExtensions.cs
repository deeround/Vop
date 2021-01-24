using Microsoft.AspNetCore.Builder;
using System;
using Vop.Api.CorsAccessor;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CorsAccessorServiceCollectionExtensions
    {
        public static IServiceCollection AddCorsAccessor(this IServiceCollection services, CorsAccessorOptions option)
        {
            if (!string.IsNullOrEmpty(option.Name))
            {
                services.AddCors(options =>
                {
                    options.AddPolicy(option.Name, option);
                });
            }
            else
            {
                services.AddCors();
            }
                

            return services;
        }

        public static IApplicationBuilder UseCorsAccessor(this IApplicationBuilder app, string policyName)
        {
            if (!string.IsNullOrEmpty(policyName))
            {
                app.UseCors(policyName);
            }
            else
            {
                app.UseCors();
            }
            
            return app;
        }
    }
}