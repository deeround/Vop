using System;

namespace Vop.Api.DependencyInjection
{
    public class InterceptorAttribute : Attribute
    {
        public InterceptorAttribute(Type proxyType)
        {
            ProxyType = proxyType;
        }

        public Type ProxyType { get; set; }
    }
}
