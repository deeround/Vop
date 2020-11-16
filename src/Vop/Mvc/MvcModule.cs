using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Vop.Api.FluentException;
using Vop.Api.Modularity;

namespace Vop.Api.FluentResult
{
    [DependsOn(typeof(FluentResultModule), typeof(FluentExceptionModule))]
    public class MvcModule : ApiModuleBase
    {
        public MvcModule(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                 .AddMvcValidation()
                 .AddMvcResult()
                 .AddMvcException()
                 .AddDynamicApiController(x =>
                 {
                     //设置参数
                 });
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
