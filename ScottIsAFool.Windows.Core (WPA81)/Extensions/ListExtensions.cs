using System;
using System.Collections.Generic;
using System.Linq;

namespace ScottIsAFool.Windows.Core.Extensions
{
    public static class ListExtensions
    {
        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            return list == null || !list.Any();
        }
        
        public static bool IsNullOrEmpty<T>(this IList<T> list)
        {
            return list == null || !list.Any();
        }

        public static bool IsNullOrEmpty<T>(this Stack<T> stack)
        {
            return stack == null || !stack.Any();
        }

        public static List<T> ToList<T>(this Array array)
        {
            return array.Cast<T>().ToList();
        } 

        public static string ToQueryString(this Dictionary<string, string> postData)
        {
            var items = postData.Select(x => string.Format	("{0}={1}", x.Key, x.Value));

            return string.Join("&", items);
        }
    }
}
