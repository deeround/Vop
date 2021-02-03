using Microsoft.AspNetCore.Builder;
using WangSql;
using WangSql.Sqlite;
using WangSql.Sqlite.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class WangSqlServiceCollectionExtensions
    {
        public static IServiceCollection AddWangSql(this IServiceCollection services, SqliteProviderOptions option)
        {
            SqliteProviderManager.Init()

            services.AddTransient<ISqlMapper, SqlMapper>();
            services.AddTransient<ISqlExe, SqlMapper>();

            return services;
        }

        public static IApplicationBuilder UseWangSql(this IApplicationBuilder app)
        {
            return app;
        }
    }
}