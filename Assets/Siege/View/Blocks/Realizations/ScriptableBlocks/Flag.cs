using System;
using Assets.Siege.CellularSpace.Agents;
using Assets.Siege.CellularSpace.Agents.Enums;
using Assets.Siege.CellularSpace.Blocks;
using Assets.Siege.CellularSpace.Blocks.Enums;
using Assets.Siege.CellularSpace.Repositories.Interfaces;
using Assets.Siege.View.Blocks.Realizations.ScriptableBlockTypes;
using Assets.Siege.View.General.MonoBehaviors;
using UnityEngine;

namespace Assets.Siege.View.Blocks.Realizations.ScriptableBlocks
{
    [CreateAssetMenu(fileName = "Flag", menuName = "ScriptableObjects/Blocks/Specific/Flag")]
    public class Flag: ActionCommitterBlock
    {
        public override BlockInfo GetInfo() => new BlockInfo(new BlockData(BlockType.Flag, BlockSolidity.Unobstructed), CommitAction, CommitAction);

        public override void Act<T>(ActableMono self, T actType, Action postAnimationAction = null)
        {
            throw new NotImplementedException();
        }

        protected override void CommitAction(FrameAgent sender, FrameBlock committer, IFrameSpaceInfo<FrameAgent> senderSpace,
            IFrameSpaceInfo<FrameBlock> committerSpace, AgentAction action)
        {
            throw new NotImplementedException();
        }

        protected override void CommitAction(FrameBlock sender, FrameBlock committer, IFrameSpaceInfo<FrameBlock> senderSpace,
            IFrameSpaceInfo<FrameBlock> committerSpace, BlockAction action)
        {
            throw new NotImplementedException();
        }
    }
}