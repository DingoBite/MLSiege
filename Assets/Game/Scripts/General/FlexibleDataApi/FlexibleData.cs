using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.General.FlexibleDataApi
{
    public class FlexibleDataContainer<TValue>
    {
        private readonly Dictionary<string, TValue> _values;

        public FlexibleDataContainer()
        {
            _values  = new Dictionary<string, TValue>();   
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
    
    /// <summary>
    /// Adding params are invoke dictionary capacity increase. All base capacity is 0, except int and object it's
    /// capacity equal 16.
    /// </summary>
    public class FlexibleData
    {
        public readonly FlexibleDataContainer<object> ObjectParams = new FlexibleDataContainer<object>(16);
        
        public readonly FlexibleDataContainer<int?> IntParams = new FlexibleDataContainer<int?>(16);
        public readonly FlexibleDataContainer<bool?> BoolParams = new FlexibleDataContainer<bool?>(0);
        public readonly FlexibleDataContainer<float?> FloatParams = new FlexibleDataContainer<float?>(0);
        
        public readonly FlexibleDataContainer<string> StringParams = new FlexibleDataContainer<string>(0);
        
        public readonly FlexibleDataContainer<Vector3?> Vector3Params = new FlexibleDataContainer<Vector3?>(0);
        public readonly FlexibleDataContainer<Vector3Int?> Vector3IntParams = new FlexibleDataContainer<Vector3Int?>(0);
        public readonly FlexibleDataContainer<Vector4?> Vector4Params = new FlexibleDataContainer<Vector4?>(0);

        public void Clear()
        {
            ObjectParams.Clear();
            
            IntParams.Clear();
            BoolParams.Clear();
            FloatParams.Clear();
            
            StringParams.Clear();
            
            Vector3Params.Clear();
            Vector3IntParams.Clear();
            Vector4Params.Clear();
        }
    }
}