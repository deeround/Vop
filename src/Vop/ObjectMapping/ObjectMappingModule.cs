using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Vop.Api.Modularity;

namespace Vop.Api.ObjectMapping
{
    public class ObjectMappingModule : ApiModuleBase
    {
        public ObjectMappingModule(IConfiguration configuration) : base(configuration)
        {
        }

        public override void Configure(IServiceCollection services)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddObjectMapping();
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseObjectMapping();
        }
    }
}
