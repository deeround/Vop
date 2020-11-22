using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vop.Api.Modularity;

namespace Vop.Api.Mvc
{
    [DependsOn(typeof(MvcCoreModule))]
    public class MvcModule : ApiModuleBase
    {
        public MvcModule(IConfiguration configuration) : base(configuration)
        {
        }

        public override void Configure(IServiceCollection services)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                 .AddMvcValidation()
                 .AddMvcResult()
                 .AddMvcException();
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}
