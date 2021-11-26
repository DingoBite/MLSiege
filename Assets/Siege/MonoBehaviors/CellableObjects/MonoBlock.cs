using Assets.Siege.Model.General.Enums;
using UnityEngine;

namespace Assets.Siege.MonoBehaviors.CellableObjects
{
    public class MonoBlock : MonoBehaviour
    {
        [SerializeField] private BlockScriptableObject _scriptableObject;

        public int Id { get; set; }

        public BlockInfo ScriptableObjectInfo() => _scriptableObject.GetInfo();
        public BlockType BlockType() => _scriptableObject.GetInfo().BlockType;

        public override string ToString()
        {
            return $"{_scriptableObject.GetInfo().BlockType}: {Id}";
        }
    }
}
