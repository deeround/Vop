using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vop.Api
{
    public static class JsonHelper
    {
        public static string Serialize(object value)
        {
            if (value == null) return null;
            if (value is string) return (string)value;
            if (value.GetType().IsValueType) return value.ToString();

            var setting = new JsonSerializerSettings();
            setting.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            setting.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

            return JsonConvert.SerializeObject(value, Formatting.None, setting);
        }
        public static T Deserialize<T>(object value)
        {
            if (value == null) return default(T);
            if (value is T) return (T)value;
            if (typeof(T) == typeof(string))
            {
                object obj = value.ToString();
                return (T)obj;
            }
            if (typeof(T).IsValueType && value is IConvertible)
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            if (value is string)
            {
                return JsonConvert.DeserializeObject<T>((string)value);
            }

            var json = Serialize(value);
            return JsonConvert.DeserializeObject<T>(json);
        }
        public static object Deserialize(object value, Type objType)
        {
            if (value == null) return null;
            if (value.GetType() == objType) return value;
            if (objType == typeof(string))
            {
                object obj = value.ToString();
                return obj;
            }
            if (objType.IsValueType && value is IConvertible)
            {
                return Convert.ChangeType(value, objType);
            }
            if (value is string)
            {
                return JsonConvert.DeserializeObject((string)value, objType);
            }

            var json = Serialize(value);
            return JsonConvert.DeserializeObject(json, objType);
        }
    }
}