using Vop.Api;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MvcExceptionServiceCollectionExtensions
    {
        public static IMvcBuilder AddMvcException(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddMvcOptions(options =>
            {
                options.Filters.Add<MvcExceptionFilter>();
            });
            return mvcBuilder;
        }
    }
}