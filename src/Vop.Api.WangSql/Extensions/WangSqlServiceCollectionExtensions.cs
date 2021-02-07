using Microsoft.AspNetCore.Builder;
using WangSql;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class WangSqlServiceCollectionExtensions
    {
        public static IServiceCollection AddWangSql(this IServiceCollection services, DbProviderOptions option)
        {
            DbProviderManager.Set(option);

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