using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Vop.Api.Modularity;

namespace Vop.Api.CorsAccessor
{
    public class CorsAccessorModule : ApiModuleBase
    {
        public CorsAccessorModule(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddCorsAccessor(builder =>
            {
                builder.Name = "localhost";
                builder.WithOrigins("http://localhost:5000")
                       .AllowAnyHeader();
            });
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCorsAccessor("localhost");
        }


    }
}
