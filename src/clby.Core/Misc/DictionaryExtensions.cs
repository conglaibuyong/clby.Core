using System.Collections.Generic;

namespace clby.Core.Misc
{
    public static class DictionaryExtensions
    {
        public static Dictionary<TKey, TValue> AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dict, IEnumerable<KeyValuePair<TKey, TValue>> values, bool ReplaceExisted = true)
        {
            if (values != null)
            {
                foreach (KeyValuePair<TKey, TValue> current in values)
                {
                    if (!dict.ContainsKey(current.Key) || ReplaceExisted)
                    {
                        dict[current.Key] = current.Value;
                    }
                }
            }
            return dict;
        }

        public static TValue TryAs<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
        {
            if (dict.ContainsKey(key))
            {
                return dict[key];
            }
            return default(TValue);
        }

        public static string TryAsString<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
        {
            return dict.TryAs(key)?.ToString();
        }




    }
}
