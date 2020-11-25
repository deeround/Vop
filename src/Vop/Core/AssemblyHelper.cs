using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace Vop.Api
{
    public static class AssemblyHelper
    {
        public static IReadOnlyList<Type> GetAllTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types;
            }
        }

        /// <summary>
        /// 获取应用有效程序集
        /// </summary>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<Assembly> GetAssemblies()
        {
            IList<Assembly> allAssemblies = new List<Assembly>();

            var dlls = DependencyContext.Default.CompileLibraries
                .Select(x => x.Name)
                .Where(name =>
                {
                    return
                    !name.StartsWith("Microsoft") &&
                    !name.StartsWith("System") &&
                    !name.StartsWith("runtime") &&
                    !name.StartsWith("mscorlib") &&
                    !name.StartsWith("netstandard") &&
                    !name.StartsWith("WindowsBase") &&
                    !name.StartsWith("Swashbuckle") &&
                    !name.StartsWith("Newtonsoft") &&
                    !name.StartsWith("Oracle") &&
                    !name.StartsWith("Npgsql") &&
                    !name.StartsWith("MySql")
                    ;
                }).ToList();

            foreach (var item in dlls)
            {
                try
                {
                    allAssemblies.Add(AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(item)));
                }
                catch
                {
                    //忽略
                }
            }

            return allAssemblies;
        }
    }
}
