using System;
using System.Text.Json;

namespace Vop.Api
{
    public static class JsonHelper
    {
        public static string Serialize(object value)
        {
            if (value == null) return null;
            if (value is string) return (string)value;
            if (value.GetType().IsValueType) return value.ToString();

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            return JsonSerializer.Serialize(value, options);
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
                return JsonSerializer.Deserialize<T>((string)value);
            }

            var json = Serialize(value);
            return JsonSerializer.Deserialize<T>(json);
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
                return JsonSerializer.Deserialize((string)value, objType);
            }

            var json = Serialize(value);
            return JsonSerializer.Deserialize(json, objType);
        }
    }
}