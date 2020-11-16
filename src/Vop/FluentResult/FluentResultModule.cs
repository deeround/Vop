using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Vop.Api.Modularity;

namespace Vop.Api.FluentResult
{
    public class FluentResultModule : ApiModuleBase
    {
        public FluentResultModule(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddFluentResult();
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseFluentResult();
        }
    }
}
