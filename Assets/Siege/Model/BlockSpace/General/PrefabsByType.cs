using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Siege.Model.BlockSpace.General.Interfaces;
using Assets.Siege.View.General.MonoBehaviors;

namespace Assets.Siege.Model.BlockSpace.General
{
    public class PrefabsByType<TKey, TPrefab> : IPrefabsByType<TKey, TPrefab>
    where TKey : Enum
    where TPrefab : ActableMono
    {
        private readonly IDictionary<TKey, TPrefab> _prefabsByTypeDictionary;

        public PrefabsByType(IDictionary<TKey, TPrefab> dictionary)
        {
            _prefabsByTypeDictionary = dictionary;
        }

        public IEnumerator<KeyValuePair<TKey, TPrefab>> GetEnumerator() => _prefabsByTypeDictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _prefabsByTypeDictionary.GetEnumerator();

        public void Add(KeyValuePair<TKey, TPrefab> item) => _prefabsByTypeDictionary.Add(item);

        public void Clear() => _prefabsByTypeDictionary.Clear();

        public bool Contains(KeyValuePair<TKey, TPrefab> item) => _prefabsByTypeDictionary.Contains(item);

        public void CopyTo(KeyValuePair<TKey, TPrefab>[] array, int arrayIndex) => _prefabsByTypeDictionary.CopyTo(array, arrayIndex);

        public bool Remove(KeyValuePair<TKey, TPrefab> item) => _prefabsByTypeDictionary.Remove(item);

        public int Count => _prefabsByTypeDictionary.Count;

        public bool IsReadOnly => _prefabsByTypeDictionary.IsReadOnly;
        public void Add(TKey key, TPrefab value) => _prefabsByTypeDictionary.Add(key, value);

        public bool ContainsKey(TKey key) => _prefabsByTypeDictionary.ContainsKey(key);

        public bool Remove(TKey key) => _prefabsByTypeDictionary.Remove(key);

        public bool TryGetValue(TKey key, out TPrefab value) => _prefabsByTypeDictionary.TryGetValue(key, out value);

        public TPrefab this[TKey key]
        {
            get => _prefabsByTypeDictionary[key];
            set => _prefabsByTypeDictionary[key] = value;
        }

        public ICollection<TKey> Keys => _prefabsByTypeDictionary.Keys;
        public ICollection<TPrefab> Values => _prefabsByTypeDictionary.Values;
    }
}