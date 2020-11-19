using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using Vop.Api.Modularity;

namespace Vop.Api.Authentication
{
    public class AuthenticationModule : ApiModuleBase
    {
        public AuthenticationModule(IConfiguration configuration) : base(configuration)
        {
        }

        public override void Configure(IServiceCollection services)
        {
            services.Configure<JwtSettingsOptions>(options =>
            {
                options.ValidateIssuerSigningKey ??= true;
                if (options.ValidateIssuerSigningKey == true)
                {
                    options.IssuerSigningKey ??= "U2FsdGVkX1+6H3D8Q//yQMhInzTdRZI9DbUGetbyaag=";
                }
                options.ValidateIssuer ??= true;
                if (options.ValidateIssuer == true)
                {
                    options.ValidIssuer ??= "dotnetchina";
                }
                options.ValidateAudience ??= true;
                if (options.ValidateAudience == true)
                {
                    options.ValidAudience ??= "powerby Fur";
                }
                options.ValidateLifetime ??= true;
                if (options.ValidateLifetime == true)
                {
                    options.ClockSkew ??= 10;
                }
                options.ExpiredTime ??= 20;
            });
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            var option = ApiApplication.GetService<IOptions<JwtSettingsOptions>>().Value;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, configureOptions =>
                    {
                        configureOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
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
                    });
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }


    }
}
