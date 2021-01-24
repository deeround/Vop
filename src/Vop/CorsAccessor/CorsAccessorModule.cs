using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using Vop.Api.Modularity;

namespace Vop.Api.CorsAccessor
{
    public class CorsAccessorModule : ApiModuleBase
    {
        public CorsAccessorModule(IConfiguration configuration) : base(configuration)
        {
        }

        public override void Configure(IServiceCollection services)
        {
            services.Configure<CorsAccessorOptions>(Configuration.GetSection("Cors"));
            services.PostConfigure<CorsAccessorOptions>(options =>
            {

            });
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            var option = ApiApplication.GetService<IOptions<CorsAccessorOptions>>();

            services.AddCorsAccessor(option.Value);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var option = ApiApplication.GetService<IOptions<CorsAccessorOptions>>();

            app.UseCorsAccessor(option.Value.Name);
        }


    }
}
