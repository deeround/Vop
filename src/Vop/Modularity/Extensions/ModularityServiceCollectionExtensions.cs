using Microsoft.AspNetCore.Builder;
using System;
using System.Linq;
using Vop.Api.Modularity;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ModularityServiceCollectionExtensions
    {
        public static IServiceCollection AddStartupModule<TApiModule>(this IServiceCollection services, IConfiguration configuration) where TApiModule : IApiModule
        {
            TryAdd(typeof(TApiModule));
            LoadModuleTypes(typeof(TApiModule));
            InitModuleTypes(configuration);

            ModuleConfigureServices(services);

            return services;
        }

        public static IApplicationBuilder UseStartupModule<TApiModule>(this IApplicationBuilder app, IWebHostEnvironment env) where TApiModule : IApiModule
        {
            ModuleConfigure(app, env);

            return app;
        }

        static void ModuleConfigureServices(IServiceCollection services)
        {
            foreach (var item in Modules)
            {
                item.ConfigureServices(services);
            }
        }

        static void ModuleConfigure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            foreach (var item in Modules)
            {
                item.Configure(app, env);
            }
        }






        static List<Type> ModuleTypes = new List<Type>();
        static List<IApiModule> Modules = new List<IApiModule>();

        static void LoadModuleTypes(Type type)
        {
            var types = GetDependTypes(type);
            foreach (var item in types)
            {
                TryAdd(item);
            }
            foreach (var item in types)
            {
                LoadModuleTypes(item);
            }
        }

        static void InitModuleTypes(IConfiguration configuration)
        {
            ModuleTypes.Reverse();
            foreach (var item in ModuleTypes)
            {
                var m = Activator.CreateInstance(item, configuration) as IApiModule;
                Modules.Add(m);
            }
        }

        static void TryAdd(Type type)
        {
            if (ModuleTypes.Contains(type))
            {
                ModuleTypes.Remove(type);
            }
            ModuleTypes.Add(type);
        }

        static bool IsModule(Type type)
        {
            return type.IsDefined(typeof(IApiModule), true);
        }

        static bool HasDependModule(Type type)
        {
            var attrs = type.GetCustomAttributes<DependsOnAttribute>(true);
            return (attrs != null && attrs.Count() > 0);
        }

        static List<Type> GetDependTypes(Type type)
        {
            var attrs = type.GetCustomAttributes<DependsOnAttribute>(true);
            if (attrs == null || attrs.Count() == 0) return new List<Type>();
            return attrs
                .SelectMany(p => p.GetDependedTypes())
                .ToList();
        }

    }
}