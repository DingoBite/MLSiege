using Assets.Siege.ScriptableObjects;
using UnityEngine;

namespace Assets.Siege.MonoBehaviors.Blocks
{
    public class MonoBlock : MonoBehaviour
    {
        [SerializeField] private InfoScriptableObject<BlockInfo> _scriptableObject;

        public int Id { get; set; }

        public BlockInfo GetInfo() => _scriptableObject.GetInfo();

        public override string ToString()
        {
            return $"{_scriptableObject.GetInfo().BlockType}: {Id}";
        }
    }
}
