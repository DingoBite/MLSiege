using System;
using Assets.Siege.Model.Agents;
using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.General.Enums;
using UnityEngine;

namespace Assets.Siege.View.Blocks.Realizations
{
    [CreateAssetMenu(fileName = "Null", menuName = "ScriptableObjects/Blocks/Null", order = 1)]
    public class Null: ScriptableBlock
    {
        public override BlockInfo GetInfo() => new BlockInfo(BlockType.Null, BlockSolidity.Unobstructed, CommitAction);
        protected override bool CommitAction(SiegeAgent sender, Block committer, IBlockSpace blockSpace, ActionType actionType) 
            => throw new Exception("Try to commit action on null block");
    }
}