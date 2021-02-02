using System;
using System.Collections.Generic;
using System.Linq;

namespace Vop.Api
{
    /// <summary>
    /// SOD类，基础数据传入类
    /// </summary>
    public class StrObjDict : Dictionary<string, object>, IDictionary<string, object>
    {
        public StrObjDict() : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        public StrObjDict(IDictionary<string, object> dictionary) : base(StringComparer.OrdinalIgnoreCase)
        {
            this.Add(dictionary);
        }

        public StrObjDict(Dictionary<string, object> dictionary) : base(StringComparer.OrdinalIgnoreCase)
        {
            this.Add(dictionary);
        }

        public new void Add(string key, object value)
        {
            if (string.IsNullOrEmpty(key)) return;

            this[key] = value;
        }

        public void Add(StrObjDict value)
        {
            if (value == null) return;

            foreach (var item in value)
            {
                Add(item.Key, item.Value);
            }
        }

        public void Add(Dictionary<string, object> value)
        {
            if (value == null) return;

            foreach (var item in value)
            {
                Add(item.Key, item.Value);
            }
        }

        public void Add(IDictionary<string, object> value)
        {
            if (value == null) return;

            foreach (var item in value)
            {
                Add(item.Key, item.Value);
            }
        }
    }

    /// <summary>
    /// SOD类，基础数据传入类扩展
    /// 获取SOD值
    /// </summary>
    public static partial class StrObjDictExtend
    {
        //
        //扩展Get方法
        //

        public static bool IsNullOrEmpty(this StrObjDict dict, string key)
        {
            var obj = Get(dict, key);

            return IsNullOrEmpty(obj);
        }

        public static object GetObject(this StrObjDict dict, string key)
        {
            var obj = Get(dict, key);

            return obj;
        }

        public static string GetString(this StrObjDict dict, string key)
        {
            var obj = Get(dict, key);

            return obj?.ToString();
        }

        public static int? GetInt(this StrObjDict dict, string key)
        {
            var obj = Get(dict, key);

            if (IsNullOrEmpty(obj)) return null;

            if (int.TryParse(obj.ToString(), out int obj1))
            {
                return obj1;
            }
            else
            {
                return null;
            }
        }

        public static double? GetDouble(this StrObjDict dict, string key)
        {
            var obj = Get(dict, key);

            if (IsNullOrEmpty(obj)) return null;

            if (double.TryParse(obj.ToString(), out double obj1))
            {
                return obj1;
            }
            else
            {
                return null;
            }
        }

        public static decimal? GetDecimal(this StrObjDict dict, string key)
        {
            var obj = Get(dict, key);

            if (IsNullOrEmpty(obj)) return null;

            if (decimal.TryParse(obj.ToString(), out decimal obj1))
            {
                return obj1;
            }
            else
            {
                return null;
            }
        }

        public static DateTime? GetDateTime(this StrObjDict dict, string key)
        {
            var obj = Get(dict, key);

            if (IsNullOrEmpty(obj)) return null;

            if (DateTime.TryParse(obj.ToString(), out DateTime obj1))
            {
                return obj1;
            }
            else
            {
                return null;
            }
        }

        public static bool? GetBool(this StrObjDict dict, string key)
        {
            var obj = Get(dict, key);

            if (IsNullOrEmpty(obj)) return null;

            if (obj.ToString().ToLower().Equals("true")) return true;
            if (obj.ToString().ToLower().Equals("false")) return false;

            if (int.TryParse(obj.ToString(), out int obj1))
            {
                return obj1 != 0;
            }
            else
            {
                return null;
            }
        }

        public static int GetInt2(this StrObjDict dict, string key)
        {
            var obj = GetInt(dict, key);

            return obj ?? 0;
        }

        public static double GetDouble2(this StrObjDict dict, string key)
        {
            var obj = GetDouble(dict, key);

            return obj ?? 0;
        }

        public static decimal GetDecimal2(this StrObjDict dict, string key)
        {
            var obj = GetDecimal(dict, key);

            return obj ?? 0;
        }

        public static DateTime GetDateTime2(this StrObjDict dict, string key)
        {
            var obj = GetDateTime(dict, key);

            return obj ?? DateTime.MinValue;
        }

        public static bool GetBool2(this StrObjDict dict, string key)
        {
            var obj = GetBool(dict, key);

            return obj ?? false;
        }

        public static StrObjDict GetSod(this StrObjDict dict, string key)
        {
            var obj = Get(dict, key);
            return JsonHelper.Deserialize<StrObjDict>(obj);
        }

        public static IList<StrObjDict> GetSods(this StrObjDict dict, string key)
        {
            var obj = Get(dict, key);
            return JsonHelper.Deserialize<IList<StrObjDict>>(obj);
        }

        #region 辅助方法

        private static object Get(StrObjDict dict, string key)
        {
            if (string.IsNullOrEmpty(key)) return null;
            if (dict.ContainsKey(key))
                return dict[key];
            else if (dict.ContainsKey(key.ToUpper()))
                return dict[key.ToUpper()];
            else if (dict.ContainsKey(key.ToLower()))
                return dict[key.ToUpper()];
            else
                return dict.FirstOrDefault(op => op.Key.ToUpper().Equals(key.ToUpper()));
        }

        private static bool IsNullOrEmpty(object obj)
        {
            if (obj == null) return true;
            else if (obj.ToString() == string.Empty) return true;
            else return false;
        }

        #endregion 辅助方法
    }
}