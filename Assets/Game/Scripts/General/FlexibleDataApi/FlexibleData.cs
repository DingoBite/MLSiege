using UnityEngine;

namespace Game.Scripts.General.FlexibleDataApi
{
    /// <summary>
    /// Adding params are invoke dictionary capacity increase. All base capacity is 0, except int. It
    /// capacity is 4.
    /// </summary>
    public class FlexibleData
    {
        public FlexibleDataContainer<object> ObjectParams => _objectParams ??= new FlexibleDataContainer<object>();
        private FlexibleDataContainer<object> _objectParams;
        
        
        public FlexibleDataContainer<int?> IntParams => _intParams ??= new FlexibleDataContainer<int?>(4);
        private FlexibleDataContainer<int?> _intParams;
        
        public FlexibleDataContainer<bool?> BoolParams => _boolParams ??= new FlexibleDataContainer<bool?>();
        private FlexibleDataContainer<bool?> _boolParams;
        
        public FlexibleDataContainer<float?> FloatParams => _floatParams ??= new FlexibleDataContainer<float?>();
        private FlexibleDataContainer<float?> _floatParams;
        
        
        public FlexibleDataContainer<string> StringParams => _stringParams ??= new FlexibleDataContainer<string>();
        private FlexibleDataContainer<string> _stringParams;
        
        
        public FlexibleDataContainer<Vector3?> Vector3Params => _vector3Params ??= new FlexibleDataContainer<Vector3?>();
        private FlexibleDataContainer<Vector3?> _vector3Params;
        
        public FlexibleDataContainer<Vector3Int?> Vector3IntParams => _vector3IntParams ??= new FlexibleDataContainer<Vector3Int?>();
        private FlexibleDataContainer<Vector3Int?> _vector3IntParams;
        
        public FlexibleDataContainer<Vector4?> Vector4Params => _vector4Params ??= new FlexibleDataContainer<Vector4?>();
        private FlexibleDataContainer<Vector4?> _vector4Params;

        public void Clear()
        {
            _objectParams?.Clear();
            
            _intParams?.Clear();
            _boolParams?.Clear();
            _floatParams?.Clear();
            
            _stringParams?.Clear();
            
            _vector3Params?.Clear();
            _vector3IntParams?.Clear();
            _vector4Params?.Clear();
        }
    }
}