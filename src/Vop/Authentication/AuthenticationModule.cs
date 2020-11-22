﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
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
                options.ValidateIssuerSigningKey = true;
                options.IssuerSigningKey = "123456123456123456";
                options.ValidateIssuer = false;
                options.ValidIssuer = "vop.api";
                options.ValidateAudience = false;
                options.ValidAudience = "vop.web";
                options.ValidateLifetime = true;
                options.ClockSkew = 300;
                options.ExpiredTime = 3600;
            });
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IJwtTokenHandler, JwtTokenHandler>();

            var option = ApiApplication.GetService<IOptions<JwtSettingsOptions>>().Value;

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
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }
    }
}