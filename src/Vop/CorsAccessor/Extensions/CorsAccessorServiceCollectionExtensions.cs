using Microsoft.AspNetCore.Builder;
using System;
using Vop.Api.CorsAccessor;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CorsAccessorServiceCollectionExtensions
    {
        //public static IServiceCollection AddCorsAccessor(this IServiceCollection services, CorsAccessorOptions option)
        //{
        //    services.AddCors(options =>
        //    {
        //        options.AddPolicy(option.Name, option);
        //    });

        //    return services;
        //}

        public static IServiceCollection AddCorsAccessor(this IServiceCollection services, Action<CorsAccessorBuilderOptions> setupAction)
        {
            var option = new CorsAccessorBuilderOptions();
            setupAction?.Invoke(option);

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