using Vop.Api.Mvc;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MvcValidationServiceCollectionExtensions
    {
        public static IMvcBuilder AddMvcValidation(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.Services.AddSingleton<IMvcValidationProvider, DefaultMvcValidationProvider>();

            // 使用自定义验证
            mvcBuilder.ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            mvcBuilder.AddMvcOptions(options =>
            {
                options.Filters.Add<MvcValidationFilter>();
            });
            return mvcBuilder;
        }
    }
}