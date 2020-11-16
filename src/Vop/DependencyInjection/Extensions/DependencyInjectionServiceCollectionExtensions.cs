using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Vop.Api;
using Vop.Api.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 依赖注入拓展类
    /// </summary>
    public static class DependencyInjectionServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, Type type)
        {
            // 注册类
            AddType(services, type);

            return services;
        }


        static void AddType(IServiceCollection services, Type type)
        {
            if (IsConventionalRegistrationDisabled(type))
            {
                return;
            }

            var dependencyAttribute = GetDependencyAttributeOrNull(type);
            var lifeTime = GetLifeTimeOrNull(type, dependencyAttribute);

            if (lifeTime == null)
            {
                return;
            }

            var serviceTypes = GetExposedServices(type);

            foreach (var serviceType in serviceTypes)
            {
                var serviceDescriptor = ServiceDescriptor.Describe(serviceType, type, lifeTime.Value);

                if (dependencyAttribute?.ReplaceServices == true)
                {
                    services.Replace(serviceDescriptor);
                }
                else if (dependencyAttribute?.TryRegister == true)
                {
                    services.TryAdd(serviceDescriptor);
                }
                else
                {
                    services.Add(serviceDescriptor);
                }

                // 注册代理类
                var proxyType = GetProxyService(type);
                if (proxyType != null)
                {
                    //RegisterDispatchProxy(services, proxyType, lifeTime.Value);
                    //serviceDescriptor = ServiceDescriptor.Describe(serviceType, provider =>
                    //{
                    //    dynamic proxy = DispatchCreateMethod.MakeGenericMethod(serviceType, proxyType).Invoke(null, null);
                    //    proxy.Services = provider;
                    //    proxy.Target = provider.GetService(type);
                    //    return proxy;
                    //}, lifeTime.Value);
                    //services.Add(serviceDescriptor);

                    serviceDescriptor = ServiceDescriptor.Describe(serviceType, provider =>
                    {
                        var proxy = ProxyGenerator.Create(serviceType, proxyType, provider.GetService(type));
                        return proxy;
                    }, lifeTime.Value);
                    services.Add(serviceDescriptor);

                }
            }
        }

        static void AddAssembly(IServiceCollection services, Assembly assembly)
        {
            var types = AssemblyHelper
                .GetAllTypes(assembly)
                .Where(
                    type => type != null &&
                            type.IsClass &&
                            !type.IsAbstract &&
                            !type.IsGenericType
                ).ToArray();

            AddTypes(services, types);
        }

        static void AddTypes(IServiceCollection services, params Type[] types)
        {
            foreach (var type in types)
            {
                AddType(services, type);
            }
        }

        static bool IsConventionalRegistrationDisabled(Type type)
        {
            return type.IsDefined(typeof(DisableConventionalRegistrationAttribute), true);
        }

        static DependencyAttribute GetDependencyAttributeOrNull(Type type)
        {
            return type.GetCustomAttribute<DependencyAttribute>(true);
        }

        static List<Type> GetExposedServices(Type type)
        {
            ExposeServicesAttribute DefaultExposeServicesAttribute =
            new ExposeServicesAttribute
            {
                IncludeDefaults = true,
                IncludeSelf = true
            };

            var attrs = type.GetCustomAttributes<ExposeServicesAttribute>(true);
            if (attrs == null || attrs.Count() == 0) attrs = new List<ExposeServicesAttribute> { DefaultExposeServicesAttribute };
            return attrs
                .SelectMany(p => p.GetExposedServiceTypes(type))
                .ToList();
        }

        static Type GetProxyService(Type type)
        {
            var attrs = type.GetCustomAttribute<InterceptorAttribute>(true);
            return attrs?.ProxyType;
        }

        static ServiceLifetime? GetLifeTimeOrNull(Type type, DependencyAttribute dependencyAttribute)
        {
            return dependencyAttribute?.Lifetime ?? GetServiceLifetimeFromClassHierarcy(type);
        }

        static ServiceLifetime? GetServiceLifetimeFromClassHierarcy(Type type)
        {
            if (typeof(ITransientDependency).GetTypeInfo().IsAssignableFrom(type))
            {
                return ServiceLifetime.Transient;
            }

            if (typeof(ISingletonDependency).GetTypeInfo().IsAssignableFrom(type))
            {
                return ServiceLifetime.Singleton;
            }

            if (typeof(IScopedDependency).GetTypeInfo().IsAssignableFrom(type))
            {
                return ServiceLifetime.Scoped;
            }

            return null;
        }

        /// <summary>
        /// 创建代理方法
        /// </summary>
        private static readonly MethodInfo DispatchCreateMethod;
        /// <summary>
        /// 已经注册的代理类
        /// </summary>
        private static readonly ConcurrentBag<(ServiceLifetime, Type)> RegisterDispatchProxies;

        private static void RegisterDispatchProxy(IServiceCollection services, Type proxyType, ServiceLifetime serviceLifetime)
        {
            if (RegisterDispatchProxies.Contains((serviceLifetime, proxyType))) return;

            var serviceDescriptor = ServiceDescriptor.Describe(typeof(DispatchProxy), proxyType, serviceLifetime);
            services.Add(serviceDescriptor);

            RegisterDispatchProxies.Add((serviceLifetime, proxyType));
        }

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static DependencyInjectionServiceCollectionExtensions()
        {
            DispatchCreateMethod = typeof(DispatchProxy).GetMethod(nameof(DispatchProxy.Create));
            RegisterDispatchProxies = new ConcurrentBag<(ServiceLifetime, Type)>();
        }
    }
}