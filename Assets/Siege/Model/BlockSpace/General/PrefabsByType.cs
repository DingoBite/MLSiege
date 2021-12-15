using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Siege.Model.BlockSpace.General.Interfaces;
using Assets.Siege.View.General.MonoBehaviors;

namespace Assets.Siege.Model.BlockSpace.General
{
    public class PrefabsByType<TType, TMono> : IPrefabsByType<TType, TMono>
    where TMono : ActableMono
    where TType : Enum
    {
        private readonly IDictionary<TType, TMono> _prefabsByTypeDictionary;

        public PrefabsByType(IDictionary<TType, TMono> dictionary)
        {
            _prefabsByTypeDictionary = dictionary;
        }

        public IEnumerator<KeyValuePair<TType, TMono>> GetEnumerator() => _prefabsByTypeDictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _prefabsByTypeDictionary.GetEnumerator();

        public void Add(KeyValuePair<TType, TMono> item) => _prefabsByTypeDictionary.Add(item);

        public void Clear() => _prefabsByTypeDictionary.Clear();

        public bool Contains(KeyValuePair<TType, TMono> item) => _prefabsByTypeDictionary.Contains(item);

        public void CopyTo(KeyValuePair<TType, TMono>[] array, int arrayIndex) => _prefabsByTypeDictionary.CopyTo(array, arrayIndex);

        public bool Remove(KeyValuePair<TType, TMono> item) => _prefabsByTypeDictionary.Remove(item);

        public int Count => _prefabsByTypeDictionary.Count;

        public bool IsReadOnly => _prefabsByTypeDictionary.IsReadOnly;
        public void Add(TType key, TMono value) => _prefabsByTypeDictionary.Add(key, value);

        public bool ContainsKey(TType key) => _prefabsByTypeDictionary.ContainsKey(key);

        public bool Remove(TType key) => _prefabsByTypeDictionary.Remove(key);

        public bool TryGetValue(TType key, out TMono value) => _prefabsByTypeDictionary.TryGetValue(key, out value);

        public TMono this[TType key]
        {
            get => _prefabsByTypeDictionary[key];
            set => _prefabsByTypeDictionary[key] = value;
        }

        public ICollection<TType> Keys => _prefabsByTypeDictionary.Keys;
        public ICollection<TMono> Values => _prefabsByTypeDictionary.Values;
    }
}