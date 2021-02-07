using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Vop.Api.Modularity;
using WangSql;

namespace Vop.Api.WangSql.Sqlite
{
    public class WangSqlSqliteModule : ApiModuleBase
    {
        public WangSqlSqliteModule(IConfiguration configuration) : base(configuration)
        {
        }

        public override void Configure(IServiceCollection services)
        {
            services.Configure<DbProviderOptions>(Configuration.GetSection("Database"));
            services.PostConfigure<DbProviderOptions>(options =>
            {
            });
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            var option = ApiApplication.GetService<IOptions<DbProviderOptions>>();

            services.AddWangSqlSqlite(option.Value);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseWangSqlSqlite();
        }
    }
}
