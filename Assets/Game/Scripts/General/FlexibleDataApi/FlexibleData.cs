using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages.CellObjects
{
    /// <summary>
    /// Adding params are invoke dictionary capacity increase. All base capacity is 0, except int and object it's
    /// capacity equal 16.
    /// </summary>
    public class FlexibleData
    {
        private readonly Dictionary<string, object> _objectParams = new Dictionary<string, object>(16);
        private readonly Dictionary<string, int?> _intParams = new Dictionary<string, int?>(16);
        private readonly Dictionary<string, bool?> _boolParams = new Dictionary<string, bool?>(0);
        private readonly Dictionary<string, float?> _floatParams = new Dictionary<string, float?>(0);
        private readonly Dictionary<string, string> _stringParams = new Dictionary<string, string>(0);
        private readonly Dictionary<string, Vector4?> _vector4Params = new Dictionary<string, Vector4?>(0);

        public void SetObjectParam(string paramName, int value) => SetParam(paramName, value, _objectParams);
        public object GetObjectParam(string paramName) => GetParam(paramName, _objectParams);
        public IEnumerable<string> ObjectParamNames => _objectParams.Keys;
        
        public void SetIntParam(string paramName, int value) => SetParam(paramName, value, _intParams);
        public int? GetIntParam(string paramName) => GetParam(paramName, _intParams);
        public IEnumerable<string> IntParamNames => _intParams.Keys;
        
        public void SetBoolParam(string paramName, bool value) => SetParam(paramName, value, _boolParams);
        public bool? GetBoolParam(string paramName) => GetParam(paramName, _boolParams);
        public IEnumerable<string> IntBoolNames => _intParams.Keys;
        
        public void SetFloatParam(string paramName, float value) => SetParam(paramName, value, _floatParams);
        public float? GetFloatParam(string paramName) => GetParam(paramName, _floatParams);
        public IEnumerable<string> IntFloatNames => _intParams.Keys;
        
        public void SetStringParam(string paramName, string value) => SetParam(paramName, value, _stringParams);
        public string GetStringParam(string paramName) => GetParam(paramName, _stringParams);
        public IEnumerable<string> IntStringNames => _intParams.Keys;

        public void SetVector4Param(string paramName, Vector4 value) => SetParam(paramName, value, _vector4Params);
        public Vector4? GetVector4Param(string paramName) => GetParam(paramName, _vector4Params);
        public IEnumerable<string> IntVector4Names => _intParams.Keys;
        
        public void Clear()
        {
            _objectParams.Clear();
            _intParams.Clear();
            _boolParams.Clear();
            _floatParams.Clear();
            _vector4Params.Clear();
            _stringParams.Clear();
        }

        private static void SetParam<TValue>(string paramName, TValue value, IDictionary<string, TValue> objects)
        {
            if (objects.ContainsKey(paramName))
                objects[paramName] = value;
            else
                objects.Add(paramName, value);
        }
        
        private static TValue GetParam<TValue>(string paramName, IDictionary<string, TValue> objects)
        {
            if (objects.TryGetValue(paramName, out var res))
                return res;
            Debug.LogError($"Can not found param with name {paramName} at {nameof(TValue)} storage");
            return default;
        }
    }
}