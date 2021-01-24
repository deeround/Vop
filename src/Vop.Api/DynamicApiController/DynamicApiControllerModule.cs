using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Vop.Api.Modularity;
using Vop.Api.Mvc;

namespace Vop.Api.DynamicApiController
{
    [DependsOn(typeof(MvcCoreModule))]
    public class DynamicApiControllerModule : ApiModuleBase
    {
        public DynamicApiControllerModule(IConfiguration configuration) : base(configuration)
        {
        }

        public override void Configure(IServiceCollection services)
        {
            services.Configure<DynamicApiControllerSettingsOptions>(Configuration.GetSection("DynamicApiController"));
            services.PostConfigure<DynamicApiControllerSettingsOptions>(options =>
            {
                options.DefaultRoutePrefix ??= "api";
                options.DefaultHttpMethod ??= "POST";
                options.LowerCaseRoute ??= false;
                options.CamelCaseRoute ??= true;
                options.KeepVerb ??= true;
                options.KeepName ??= false;
                options.CamelCaseSeparator ??= "";
                options.VersionSeparator ??= "@";
                options.ModelToQuery ??= true;
                options.SupportedMvcController ??= true;
                options.AbandonControllerAffixes ??= new string[]
                {
                    "Services",
                    "Service",
                    "ApiServices",
                    "ApiService",
                    "Controllers",
                    "Controller",
                    "ApiControllers",
                    "ApiController",
                };
                options.AbandonActionAffixes ??= new string[]
                {
                    "Async"
                };
            });
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            var option = ApiApplication.GetService<IOptions<DynamicApiControllerSettingsOptions>>();

            services.AddDynamicApiControllers(option.Value);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }


    }
}
