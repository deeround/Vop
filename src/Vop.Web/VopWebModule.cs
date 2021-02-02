using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vop.Api.Authentication;
using Vop.Api.AutoMapper;
using Vop.Api.Caching;
using Vop.Api.CorsAccessor;
using Vop.Api.DynamicApiController;
using Vop.Api.Modularity;
using Vop.Api.Mvc;
using Vop.Api.RedisCache;
using Vop.Api.Swagger;
using Vop.Api.WangSql;

namespace Vop.Web
{
    [DependsOn(
        typeof(MvcModule),
        typeof(DynamicApiControllerModule),
        typeof(CorsAccessorModule),
        typeof(SwaggerModule),
        typeof(CachingModule),
        typeof(AutoMapperModule),
        typeof(AuthenticationModule),
        typeof(RedisCacheModule),
        typeof(WangSqlModule)
        )]
    public class VopWebModule : ApiModuleBase
    {
        public VopWebModule(IConfiguration configuration) : base(configuration)
        {
        }

        public override void Configure(IServiceCollection services)
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
