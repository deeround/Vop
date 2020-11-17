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

        public override void Configure(IServiceCollection services)
        {
            services.Configure<ErrorCodeOptions>(builder =>
            {
                builder
                .Add(-1, "未知错误")
                .Add(4000, "系统错误")
                .Add(4001, "入参验证失败");
            });
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            var option = ApiApplication.GetService<IOptions<ErrorCodeOptions>>();

            services.AddFluentException(option.Value);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseFluentException();
        }
    }
}
