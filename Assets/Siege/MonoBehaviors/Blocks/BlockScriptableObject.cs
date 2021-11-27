using Assets.Siege.Model.General.Enums;
using Assets.Siege.ScriptableObjects;
using UnityEngine;

namespace Assets.Siege.MonoBehaviors.CellableObjects
{
    [CreateAssetMenu(fileName = "CellableObject", menuName = "ScriptableObjects/CellableObjects", order = 1)]
    public class BlockScriptableObject : InfoScriptableObject<BlockInfo>
    {
        [SerializeField] private BlockType _blockType;
        [SerializeField] private BlockSolidity _blockSolidity;

        public override BlockInfo GetInfo() => new BlockInfo(_blockType, _blockSolidity);
    }
}
