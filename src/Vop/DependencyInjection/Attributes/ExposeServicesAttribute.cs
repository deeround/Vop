﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Vop.Api.DependencyInjection
{
    public class ExposeServicesAttribute : Attribute
    {
        public Type[] ServiceTypes { get; }

        public bool IncludeDefaults { get; set; }

        public bool IncludeSelf { get; set; }

        public ExposeServicesAttribute(params Type[] serviceTypes)
        {
            ServiceTypes = serviceTypes ?? new Type[0];
        }

        public Type[] GetExposedServiceTypes(Type targetType)
        {
            var serviceList = ServiceTypes.ToList();

            if (IncludeSelf)
            {
                if (!serviceList.Contains(targetType))
                {
                    serviceList.Add(targetType);
                }
            }

            if (IncludeDefaults)
            {
                foreach (var type in GetDefaultServices(targetType))
                {
                    if (!serviceList.Contains(type))
                    {
                        serviceList.Add(type);
                    }
                }
            }

            return serviceList.ToArray();
        }

        private static List<Type> GetDefaultServices(Type type)
        {
            var serviceTypes = new List<Type>();

            foreach (var interfaceType in type.GetTypeInfo().GetInterfaces())
            {
                var interfaceName = interfaceType.Name;

                if (interfaceName.StartsWith("I"))
                {
                    interfaceName = interfaceName.Right(interfaceName.Length - 1);
                }

                if (type.Name.EndsWith(interfaceName))
                {
                    serviceTypes.Add(interfaceType);
                }
            }

            return serviceTypes;
        }
    }
}