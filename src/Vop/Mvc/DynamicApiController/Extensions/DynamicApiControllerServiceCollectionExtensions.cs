using Vop.Api.DynamicApiController;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///动态接口控制器拓展类
    /// </summary>
    public static class DynamicApiControllerServiceCollectionExtensions
    {
        ///// <summary>
        ///// 添加动态接口控制器服务
        ///// </summary>
        ///// <param name="mvcBuilder">Mvc构建器</param>
        ///// <returns>Mvc构建器</returns>
        //public static IMvcBuilder AddDynamicApiController(this IMvcBuilder mvcBuilder, DynamicApiControllerSettingsOptions option)
        //{
        //    var services = mvcBuilder.Services;

        //    var partManager = services.FirstOrDefault(s => s.ServiceType == typeof(ApplicationPartManager)).ImplementationInstance as ApplicationPartManager
        //        ?? throw new InvalidOperationException($"`{nameof(AddDynamicApiController)}` must be invoked after `{nameof(MvcServiceCollectionExtensions.AddControllers)}`");

        //    // 添加控制器特性提供器
        //    partManager.FeatureProviders.Add(new DynamicApiControllerFeatureProvider());

        //    // 配置 Mvc 选项
        //    mvcBuilder.AddMvcOptions(options =>
        //    {
        //        // 添加应用模型转换器
        //        options.Conventions.Add(new DynamicApiControllerApplicationModelConvention(option));
        //    });

        //    // 添加丰富类型支持
        //    mvcBuilder.AddNewtonsoftJson();

        //    // 添加 Xml 支持
        //    mvcBuilder.AddXmlDataContractSerializerFormatters();

        //    return mvcBuilder;
        //}

        /// <summary>
        /// 添加动态接口控制器服务
        /// </summary>
        /// <param name="mvcBuilder">Mvc构建器</param>
        /// <returns>Mvc构建器</returns>
        public static IMvcBuilder AddDynamicApiController(this IMvcBuilder mvcBuilder, Action<DynamicApiControllerSettingsOptions> setupAction)
        {
            var option = new DynamicApiControllerSettingsOptions();
            setupAction?.Invoke(option);

            var services = mvcBuilder.Services;

            var partManager = services.FirstOrDefault(s => s.ServiceType == typeof(ApplicationPartManager)).ImplementationInstance as ApplicationPartManager
                ?? throw new InvalidOperationException($"`{nameof(AddDynamicApiController)}` must be invoked after `{nameof(MvcServiceCollectionExtensions.AddControllers)}`");

            // 添加控制器特性提供器
            partManager.FeatureProviders.Add(new DynamicApiControllerFeatureProvider());

            // 配置 Mvc 选项
            mvcBuilder.AddMvcOptions(options =>
            {
                // 添加应用模型转换器
                options.Conventions.Add(new DynamicApiControllerApplicationModelConvention(option));
            });

            // 添加丰富类型支持
            mvcBuilder.AddNewtonsoftJson();

            // 添加 Xml 支持
            mvcBuilder.AddXmlDataContractSerializerFormatters();

            return mvcBuilder;
        }
    }
}