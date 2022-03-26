using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.General.FlexibleDataApi
{
    public class FlexibleDataContainer<TValue>
    {
        private readonly Dictionary<string, TValue> _values;

        public FlexibleDataContainer()
        {
            _values = new Dictionary<string, TValue>();
        }
        
        public FlexibleDataContainer(int capacity)
        {
            _values  = new Dictionary<string, TValue>(capacity);   
        }

        public void SetParam(string paramName, TValue value)
        {
            if (_values.ContainsKey(paramName))
                _values[paramName] = value;
            else
                _values.Add(paramName, value);
        }

        public TValue GetParam(string paramName)
        {
            if (_values.TryGetValue(paramName, out var res))
                return res;
            Debug.LogError($"Can not found param with name {paramName} at {nameof(TValue)} storage");
            return default;
        }
        
        public IEnumerable<string> ParamNames => _values.Keys;
        
        public void Clear() => _values.Clear();
    }
}