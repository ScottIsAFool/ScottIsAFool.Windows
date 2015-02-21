using System;
using System.Collections.Generic;
using System.Linq;

namespace ScottIsAFool.Windows.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddIfNotNull(this Dictionary<string, string> postData, string key, int? item)
        {
            if (item.HasValue)
            {
                postData.Add(key, item.Value.ToString());
            }
        }

        public static void AddIfNotNull(this Dictionary<string, string> postData, string key, double? item)
        {
            if (item.HasValue)
            {
                postData.Add(key, item.Value.ToString());
            }
        }

        public static void AddIfNotNull(this Dictionary<string, string> postData, string key, DateTime? item)
        {
            if (item.HasValue)
            {
                postData.Add(key, item.Value.ToString());
            }
        }

        public static void AddIfNotNull<TEnum>(this Dictionary<string, string> postData, string key, TEnum? item) where TEnum : struct
        {
            if (item.HasValue)
            {
                postData.Add(key, item.Value.GetDescription().ToLower());
            }
        }

        public static void AddIfNotNull(this Dictionary<string, string> postData, string key, bool? item, bool isBinary = false)
        {
            if (item.HasValue)
            {
                if (isBinary)
                {
                    postData.Add(key, item.Value ? "1" : "0");
                }
                else
                {
                    postData.Add(key, item.Value.ToString());
                }
            }
        }

        public static void AddIfNotNull(this Dictionary<string, string> postData, string key, string item)
        {
            if (!string.IsNullOrEmpty(item))
            {
                postData.Add(key, item);
            }
        }

        public static void AddIfNotNull<T>(this Dictionary<string, string> postData, string key, List<T> item)
        {
            if (item != null && item.Any())
            {
                var data = string.Format("[{0}]", string.Join(",", item));
                postData.Add(key, data);
            }
        }
    }
}