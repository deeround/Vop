using Microsoft.AspNetCore.Authentication.JwtBearer;
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
            var option = ApiApplication.GetService<IOptions<JwtSettingsOptions>>().Value;

            services.AddAuthentication(option);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }
    }
}
