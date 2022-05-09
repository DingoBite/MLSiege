using System.Collections.Generic;

namespace Game.Scripts.General.Extensions
{
    public static class DictionaryExtension
    {
        public static void AddOrEdit<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
                dictionary[key] = value;
            else
                dictionary.Add(key, value);
        }
    }
}