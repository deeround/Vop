using Microsoft.AspNetCore.Builder;
using Vop.Api.WangSql;
using WangSql;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AutoMapperServiceCollectionExtensions
    {
        public static IServiceCollection AddWangSql(this IServiceCollection services, WangSqlOptions option)
        {
            DbProviderManager.Set(
                option.Name,
                option.ConnectionString,
                option.ConnectionType,
                option.UseParameterPrefixInSql,
                option.UseParameterPrefixInParameter,
                option.ParameterPrefix,
                option.UseQuotationInSql,
                option.Debug
                );

            return services;
        }

        public static IApplicationBuilder UseWangSql(this IApplicationBuilder app)
        {
            return app;
        }
    }
}