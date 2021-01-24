using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vop.Api.Modularity;

namespace Vop.Api
{
    /// <summary>
    /// 应用全局静态服务
    /// </summary>
    public static class ApiApplication
    {
        /// <summary>
        /// List of services registered to this application.
        /// Can not add new services to this collection after application initialize.
        /// </summary>
        public static IServiceCollection Services { get; set; }

        /// <summary>
        /// Reference to the root service provider used by the application.
        /// This can not be used before initialize the application.
        /// </summary>
        public static IServiceProvider ServiceProvider => Services?.BuildServiceProvider();

        /// <summary>
        /// 所有使用的模块
        /// </summary>
        public static IList<IApiModule> ApiModules { get; set; }

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static object GetService(Type type)
        {
            return ServiceProvider?.GetService(type);
        }

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <returns></returns>
        public static T GetService<T>()
        {
            return ServiceProvider == null ? default(T) : ServiceProvider.GetService<T>();
        }

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetRequiredService(Type type)
        {
            return ServiceProvider?.GetRequiredService(type);
        }

        /// <summary>
        /// 获取作用域服务
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetRequestService(Type type)
        {
            var serviceProvider = GetService<IHttpContextAccessor>()?.HttpContext?.RequestServices;
            return serviceProvider == null ? null : serviceProvider.GetService(type);
        }

        /// <summary>
        /// 获取作用域服务
        /// </summary>
        /// <returns></returns>
        public static T GetRequestService<T>()
        {
            var serviceProvider = GetService<IHttpContextAccessor>()?.HttpContext?.RequestServices;
            return serviceProvider == null ? default(T) : serviceProvider.GetService<T>();
        }
    }
}
