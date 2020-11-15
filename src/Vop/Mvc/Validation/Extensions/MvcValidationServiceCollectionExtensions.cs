using Vop.Api;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MvcValidationServiceCollectionExtensions
    {
        public static IMvcBuilder AddMvcValidation(this IMvcBuilder mvcBuilder)
        {
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