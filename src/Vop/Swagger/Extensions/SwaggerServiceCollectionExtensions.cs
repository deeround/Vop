using Vop.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SwaggerServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Api", new OpenApiInfo()
                {
                    Title = "Api",
                    Version = "v1.0",
                    Description = "测试接口"
                });
                //添加设置Token的按钮
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "请输入带有Bearer的Token：Bearer {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
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

        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "{documentName}/swagger.json";
            });
            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/Api/swagger.json", "Api");
            });

            return app;
        }
    }
}