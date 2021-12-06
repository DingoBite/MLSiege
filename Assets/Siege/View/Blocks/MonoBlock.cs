using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.View.General;
using UnityEngine;

namespace Assets.Siege.View.Blocks
{
    public class MonoBlock : MonoBehaviour
    {
        [SerializeField] private InfoScriptableObject<BlockInfo> _scriptableObject;

        public int Id { get; set; }

        private BlockInfo _blockInfo;
        public BlockInfo GetInfo() => _blockInfo ??= _scriptableObject.GetInfo();

        public override string ToString()
        {
            return $"{_scriptableObject.GetInfo().BlockType}: {Id}";
        }
    }
}
