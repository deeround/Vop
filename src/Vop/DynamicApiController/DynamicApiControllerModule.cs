using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using Vop.Api.Modularity;
using Vop.Api.Mvc;

namespace Vop.Api.DynamicApiController
{
    [DependsOn(typeof(MvcModule))]
    public class DynamicApiControllerModule : ApiModuleBase
    {
        public DynamicApiControllerModule(IConfiguration configuration) : base(configuration)
        {
        }

        public override void Configure(IServiceCollection services)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddDynamicApiControllers();
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }


    }
}
