using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSearchApp.Helpers
{
    public static class CacheHelper
    {
        private static readonly Dictionary<string, object> _cache = new();

        public static T Get<T>(string key) where T : class
        {
            _cache.TryGetValue(key, out var value);
            return value as T;
        }

        public static void Set(string key, object value)
        {
            _cache[key] = value;
        }

        public static bool Contains(string key) => _cache.ContainsKey(key);
    }
}
