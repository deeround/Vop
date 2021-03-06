﻿using Microsoft.AspNetCore.Builder;
using WangSql;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class WangSqlServiceCollectionExtensions
    {
        public static IServiceCollection AddWangSql(this IServiceCollection services, DbProviderOptions option)
        {
            DbProviderManager.Set(option);

            services.AddScoped<ISqlMapper, SqlMapper>();
            services.AddScoped<ISqlExe, SqlMapper>();

            return services;
        }

        public static IApplicationBuilder UseWangSql(this IApplicationBuilder app)
        {
            return app;
        }
    }
}