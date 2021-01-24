using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Vop.Api.Caching;
using Vop.Api.Modularity;

namespace Vop.Api.RedisCache
{
    [DependsOn(typeof(CachingModule))]
    public class RedisCacheModule : ApiModuleBase
    {
        public RedisCacheModule(IConfiguration configuration) : base(configuration)
        {
        }

        public override void Configure(IServiceCollection services)
        {
            services.Configure<RedisCacheOptions>(Configuration.GetSection("RedisCache"));
            services.PostConfigure<RedisCacheOptions>(options =>
            {
                options.InstanceName ??= "vop:";
            });
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            var option = ApiApplication.GetService<IOptions<RedisCacheOptions>>();

            services.AddRedisCache(option.Value);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRedisCache();
        }
    }
}
