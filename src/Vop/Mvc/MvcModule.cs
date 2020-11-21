using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vop.Api.FluentException;
using Vop.Api.FluentResult;
using Vop.Api.Modularity;

namespace Vop.Api.Mvc
{
    [DependsOn(typeof(FluentResultModule), typeof(FluentExceptionModule))]
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
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ICancellationTokenProvider, HttpContextCancellationTokenProvider>();

            services.AddControllers()
                 .AddNewtonsoftJson()
                 .AddMvcValidation()
                 .AddMvcResult()
                 .AddMvcException();
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
