using Assets.Siege.Model.Agents;
using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.General.Enums;
using Assets.Siege.View.Blocks.ScriptableBlockTypes;
using UnityEngine;

namespace Assets.Siege.View.Blocks.Realizations
{
    [CreateAssetMenu(fileName = "Stone", menuName = "ScriptableObjects/Blocks/Stone")]
    public class Stone: DestructibleBlock
    {
        public override BlockInfo GetInfo() => new BlockInfo(BlockType.Stone, BlockSolidity.Solid, CommitAction);
        protected override bool CommitAction(SiegeAgent sender, Block committer, IBlockSpace blockSpace, ActionType actionType) => true;
    }
}