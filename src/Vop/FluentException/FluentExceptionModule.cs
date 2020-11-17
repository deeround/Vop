using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using Vop.Api.FluentResult;
using Vop.Api.Modularity;

namespace Vop.Api.FluentException
{
    [DependsOn(typeof(FluentResultModule))]
    public class FluentExceptionModule : ApiModuleBase
    {
        public FluentExceptionModule(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ErrorCodeOptions>(builder =>
            {

            });

            var option = ApiApplication.GetService<IOptions<ErrorCodeOptions>>();

            services.AddFluentException(option.Value);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseFluentException();
        }
    }
}
