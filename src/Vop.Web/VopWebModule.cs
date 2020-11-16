﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vop.Api.CorsAccessor;
using Vop.Api.FluentResult;
using Vop.Api.Modularity;
using Vop.Api.Swagger;

namespace Vop.Web
{
    [DependsOn(
        typeof(MvcModule), 
        typeof(CorsAccessorModule), 
        typeof(ApiSwaggerModule),
        typeof(CachingModule))]
    public class VopWebModule : ApiModuleBase
    {
        public VopWebModule(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }


    }
}
