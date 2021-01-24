using Vop.Api.Mvc;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MvcExceptionServiceCollectionExtensions
    {
        public static IMvcBuilder AddMvcException(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.Services.AddSingleton<IMvcExceptionProvider, DefaultMvcExceptionProvider>();

            mvcBuilder.AddMvcOptions(options =>
            {
                options.Filters.Add<MvcExceptionFilter>();
            });
            return mvcBuilder;
        }
    }
}