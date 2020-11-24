using Vop.Api.Mvc;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MvcResultServiceCollectionExtensions
    {
        public static IMvcBuilder AddMvcResult(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.Services.AddSingleton<IMvcResultProvider, DefaultMvcResultProvider>();

            mvcBuilder.AddMvcOptions(options =>
            {
                options.Filters.Add<MvcResultFilter>();
            });
            return mvcBuilder;
        }
    }
}