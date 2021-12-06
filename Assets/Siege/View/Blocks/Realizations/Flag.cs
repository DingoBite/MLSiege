using Assets.Siege.Model.Agents;
using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.General.Enums;
using UnityEngine;

namespace Assets.Siege.View.Blocks.Realizations
{
    [CreateAssetMenu(fileName = "Flag", menuName = "ScriptableObjects/Blocks/Flag")]
    public class Flag: ScriptableBlock
    {
        public override BlockInfo GetInfo() => new BlockInfo(BlockType.Flag, BlockSolidity.Unobstructed, CommitAction);

        protected override bool CommitAction(SiegeAgent sender, Block committer, IBlockSpace blockSpace, ActionType actionType) 
            => actionType == ActionType.Use;
    }
}