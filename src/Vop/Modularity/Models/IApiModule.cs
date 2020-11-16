using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vop.Api.Modularity
{
    public interface IApiModule
    {
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        void ConfigureServices(IServiceCollection services);

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        void Configure(IApplicationBuilder app, IWebHostEnvironment env);
    }
}
