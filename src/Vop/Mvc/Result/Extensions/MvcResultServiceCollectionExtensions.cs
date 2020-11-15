using Vop.Api;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MvcResultServiceCollectionExtensions
    {
        public static IMvcBuilder AddMvcResult(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddMvcOptions(options =>
            {
                options.Filters.Add<MvcResultFilter>();
            });
            return mvcBuilder;
        }
    }
}