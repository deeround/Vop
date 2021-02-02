using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Vop.Api.Caching;
using Vop.Api.Modularity;

namespace Vop.Api.WangSql
{
    public class WangSqlModule : ApiModuleBase
    {
        public WangSqlModule(IConfiguration configuration) : base(configuration)
        {
        }

        public override void Configure(IServiceCollection services)
        {
            services.Configure<WangSqlOptions>(Configuration.GetSection("Database"));
            services.PostConfigure<WangSqlOptions>(options =>
            {
            });
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            var option = ApiApplication.GetService<IOptions<WangSqlOptions>>();

            services.AddWangSql(option.Value);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseWangSql();
        }
    }
}
