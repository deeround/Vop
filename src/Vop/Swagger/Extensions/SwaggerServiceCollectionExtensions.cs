using Vop.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using Vop.Api.Swagger;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SwaggerServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, SwaggerSettingsOptions options)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Api", new OpenApiInfo()
                {
                    Title = options.Title,
                    Version = options.Version,
                    Description = options.Description
                });
                //添加Authorization
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                //添加全局token
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>()
                    }
                });
                //Set the comments path for the swagger json and ui.
                var xmlPath = System.AppDomain.CurrentDomain.BaseDirectory;
                var files = System.IO.Directory.GetFiles(xmlPath, "*.xml");
                foreach (var item in files)
                {
                    c.IncludeXmlComments(item, true);
                }
            });


            return services;
        }

        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, SwaggerSettingsOptions options)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "{documentName}/swagger.json";
            });
            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/{options.Title}/swagger.json", "Api");
            });

            return app;
        }
    }
}