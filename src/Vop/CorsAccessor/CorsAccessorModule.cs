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
            services.Configure<CorsAccessorBuilderOptions>(builder =>
            {
                builder.Name = "localhost";
                builder.WithOrigins("http://localhost:5000")
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            var option = ApiApplication.GetService<IOptions<CorsAccessorBuilderOptions>>();

            services.AddCorsAccessor(option.Value);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCorsAccessor("localhost");
        }


    }
}
