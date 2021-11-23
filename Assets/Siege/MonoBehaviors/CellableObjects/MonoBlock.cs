using UnityEngine;

namespace Assets.Siege.MonoBehaviors.CellableObjects
{
    public class MonoBlock : MonoBehaviour
    {
        [SerializeField] private BlockScriptableObject _scriptableObject;

        public readonly int Id;

        public BlockInfo ScriptableObjectInfo() => _scriptableObject.GetInfo();
    }
}
