using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using Vop.Api.Modularity;

namespace Vop.Api.Swagger
{
    public class SwaggerModule : ApiModuleBase
    {
        public SwaggerModule(IConfiguration configuration) : base(configuration)
        {
        }

        public override void Configure(IServiceCollection services)
        {
            services.Configure<SwaggerSettingsOptions>(options =>
            {
                options.Title = "api";
                options.Version = null;
                options.Description = "api接口文档";
            });
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            var options = ApiApplication.GetService<IOptions<SwaggerSettingsOptions>>();

            services.AddSwagger(options.Value);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var options = ApiApplication.GetService<IOptions<SwaggerSettingsOptions>>();

            app.UseSwagger(options.Value);
        }
    }
}
