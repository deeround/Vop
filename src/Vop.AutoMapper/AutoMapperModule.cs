using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Vop.Api.Modularity;
using Vop.Api.ObjectMapping;

namespace Vop.Api.AutoMapper
{
    [DependsOn(typeof(ObjectMappingModule))]
    public class AutoMapperModule : ApiModuleBase
    {
        public AutoMapperModule(IConfiguration configuration) : base(configuration)
        {
        }

        public override void Configure(IServiceCollection services)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapperObjectMapper();
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAutoMapperObjectMapper();
        }
    }
}
