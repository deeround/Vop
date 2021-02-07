using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vop.Web.Models;
using WangSql;

namespace Vop.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddStartupModule<VopWebModule>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISqlMapper sqlMapper)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStartupModule<VopWebModule>(env);

            try
            {
                sqlMapper.SqlFactory.DbProvider.SetTableMaps(new List<Type>() { typeof(UserInfo) });
                sqlMapper.Migrate().CreateTable();
            }
            catch (Exception ex)
            {
                throw new Exception("初始化数据库失败：" + ex.Message);
            }
        }
    }
}
