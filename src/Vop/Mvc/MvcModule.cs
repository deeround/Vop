using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using Vop.Api.DynamicApiController;
using Vop.Api.FluentException;
using Vop.Api.FluentResult;
using Vop.Api.Modularity;

namespace Vop.Api.Mvc
{
    [DependsOn(typeof(FluentResultModule), typeof(FluentExceptionModule))]
    public class MvcModule : ApiModuleBase
    {
        public MvcModule(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ICancellationTokenProvider, HttpContextCancellationTokenProvider>();

            services.Configure<DynamicApiControllerSettingsOptions>(builder =>
            {

            });

            var dynamicApiControllerSettingsOption = ApiApplication.GetService<IOptions<DynamicApiControllerSettingsOptions>>();

            services.AddControllers()
                 .AddMvcValidation()
                 .AddMvcResult()
                 .AddMvcException()
                 .AddDynamicApiController(dynamicApiControllerSettingsOption.Value);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
