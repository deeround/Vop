using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using WangSql;
using WangSql.Sqlite;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class WangSqlSqliteServiceCollectionExtensions
    {
        public static IServiceCollection AddWangSqlSqlite(this IServiceCollection services, DbProviderOptions option)
        {
            SqliteProviderManager.Set(option);

            services.AddScoped<ISqlMapper, SqlMapper>();
            services.AddScoped<ISqlExe, SqlMapper>();

            return services;
        }

        public static IApplicationBuilder UseWangSqlSqlite(this IApplicationBuilder app)
        {
            return app;
        }
    }
}