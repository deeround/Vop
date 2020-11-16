using System;
using System.Reflection;
using Vop.Api.DependencyInjection;

namespace Vop.Web.Services
{
    public class LogDispatchProxy : IInterceptor
    {
        public object Intercept(MethodInfo method, object[] parameters)
        {
            Console.WriteLine("aaaa");
            return null;
        }
    }
}