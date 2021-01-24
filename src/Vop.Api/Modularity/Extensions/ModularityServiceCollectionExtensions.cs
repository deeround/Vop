using Microsoft.AspNetCore.Builder;
using System;
using System.Linq;
using Vop.Api.Modularity;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Vop.Api;
using Vop.Api.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ModularityServiceCollectionExtensions
    {
        public static IServiceCollection AddStartupModule<TApiModule>(this IServiceCollection services, IConfiguration configuration) where TApiModule : IApiModule
        {
            InitModules(typeof(TApiModule), configuration);

            ModuleConfigure(services);
            ModuleConfigureServices(services);

            return services;
        }

        public static IApplicationBuilder UseStartupModule<TApiModule>(this IApplicationBuilder app, IWebHostEnvironment env) where TApiModule : IApiModule
        {
            ModuleConfigure(app, env);

            return app;
        }

        static void ModuleConfigure(IServiceCollection services)
        {
            foreach (var item in ApiApplication.ApiModules)
            {
                item.Configure(services);
            }
        }

        static void ModuleConfigureServices(IServiceCollection services)
        {
            foreach (var item in ApiApplication.ApiModules)
            {
                item.ConfigureServices(services);
            }
        }

        static void ModuleConfigure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            foreach (var item in ApiApplication.ApiModules)
            {
                item.Configure(app, env);
            }
        }








        static void LoadModules(Type type, List<Type> moduleTypes)
        {
            var types = GetDependModules(type);
            types.Reverse();
            foreach (var item in types)
            {
                TryAdd(item, moduleTypes);
            }
            foreach (var item in types)
            {
                LoadModules(item, moduleTypes);
            }
        }

        static List<IApiModule> InitModules(Type startModule, IConfiguration configuration)
        {
            List<Type> moduleTypes = new List<Type>();
            List<IApiModule> modules = new List<IApiModule>();

            TryAdd(startModule, moduleTypes);
            LoadModules(startModule, moduleTypes);
            TryAdd(typeof(CoreModule), moduleTypes);
            TryAdd(typeof(DependencyInjectionModule), moduleTypes);

            moduleTypes.Reverse();
            foreach (var item in moduleTypes)
            {
                if (IsModule(item))
                {
                    var m = Activator.CreateInstance(item, configuration) as IApiModule;
                    modules.Add(m);
                }
            }

            ApiApplication.ApiModules = modules;

            return modules;
        }

        static void TryAdd(Type type, List<Type> moduleTypes)
        {
            if (moduleTypes.Contains(type))
            {
                moduleTypes.Remove(type);
            }
            moduleTypes.Add(type);
        }

        static bool IsModule(Type type)
        {
            return typeof(IApiModule).IsAssignableFrom(type);
        }

        static List<Type> GetDependModules(Type type)
        {
            var attrs = type.GetCustomAttributes<DependsOnAttribute>(true);
            if (attrs == null || attrs.Count() == 0) return new List<Type>();
            return attrs
                .SelectMany(p => p.GetDependedTypes())
                .ToList();
        }

    }
}