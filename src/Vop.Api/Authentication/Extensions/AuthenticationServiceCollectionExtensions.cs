using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using Vop.Api.Authentication;
using Vop.Api.Caching;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AuthenticationServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services, JwtSettingsOptions option)
        {
            services.AddSingleton<IJwtTokenHandler, JwtTokenHandler>();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, configureOptions =>
                    {
                        configureOptions.TokenValidationParameters = new TokenValidationParameters()
                        {
                            // 验证签发方密钥
                            ValidateIssuerSigningKey = option.ValidateIssuerSigningKey.Value,
                            // 签发方密钥
                            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(option.IssuerSigningKey)),
                            // 验证签发方
                            ValidateIssuer = option.ValidateIssuer.Value,
                            // 设置签发方
                            ValidIssuer = option.ValidIssuer,
                            // 验证签收方
                            ValidateAudience = option.ValidateAudience.Value,
                            // 设置接收方
                            ValidAudience = option.ValidAudience,
                            // 验证生存期
                            ValidateLifetime = option.ValidateLifetime.Value,
                            // 过期时间容错值
                            ClockSkew = TimeSpan.FromSeconds(option.ClockSkew.Value),
                        };
                        configureOptions.SaveToken = true;
                    });

            return services;
        }
    }
}