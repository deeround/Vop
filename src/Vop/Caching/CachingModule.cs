using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using Vop.Api;
using Vop.Api.Modularity;

namespace Vop.Api.Caching
{
    public class CachingModule : ApiModuleBase
    {
        public CachingModule(IConfiguration configuration) : base(configuration)
        {
        }

        public override void Configure(IServiceCollection services)
        {
            services.Configure<DistributedCacheOptions>(cacheOptions =>
            {
                cacheOptions.GlobalCacheEntryOptions.SlidingExpiration = TimeSpan.FromMinutes(20);
            });
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            var option = ApiApplication.GetService<IOptions<DistributedCacheOptions>>();

            services.AddDistributedCache(option.Value);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDistributedCache();
        }
    }
}
