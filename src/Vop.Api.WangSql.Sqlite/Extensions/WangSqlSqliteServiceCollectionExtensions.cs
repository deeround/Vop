using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using WangSql;
using WangSql.Sqlite;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class WangSqlSqliteServiceCollectionExtensions
    {
        public static IServiceCollection AddWangSqlSqlite(this IServiceCollection services, DbProviderOptions option, IList<Type> tableMaps = null, bool autoCreateTable = false)
        {
            SqliteProviderManager.Set(option, tableMaps, autoCreateTable);

            services.AddTransient<ISqlMapper, SqlMapper>();
            services.AddTransient<ISqlExe, SqlMapper>();

            return services;
        }

        public static IApplicationBuilder UseWangSqlSqlite(this IApplicationBuilder app)
        {
            return app;
        }
    }
}