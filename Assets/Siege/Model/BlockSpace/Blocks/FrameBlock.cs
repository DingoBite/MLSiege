using Assets.Siege.Model.BlockSpace.Agents;
using Assets.Siege.Model.BlockSpace.Agents.Enums;
using Assets.Siege.Model.BlockSpace.Blocks.Enums;
using Assets.Siege.Model.BlockSpace.Features;
using Assets.Siege.Model.BlockSpace.General.CellObjects;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.Blocks;
using UnityEngine;

namespace Assets.Siege.Model.BlockSpace.Blocks
{
    public class FrameBlock: CellObjectFrame<FrameBlock, Block, MonoBlock, BlockFeatures>
    {
        public FrameBlock(IFrameSpaceContext<FrameBlock> context, Block block, MonoBlock monoBlock, Vector3Int coords)
            : base(context, block, monoBlock, coords)
        {
        }

        public void CommitAction(FrameAgent sender, AgentAction action, IFrameSpaceInfo<FrameAgent> senderSpace) 
            => PostAnimationCommit(action, () => _cellObj.CommitAction(sender, this, senderSpace, _context, action));

        public void CommitAction(FrameBlock sender, BlockAction action, IFrameSpaceInfo<FrameBlock> senderSpace) 
            => PostAnimationCommit(action, () => _cellObj.CommitAction(sender, this, senderSpace, _context, action));

        public void CommitAction(FrameBlock sender, BlockAction action) 
            => PostAnimationCommit(action, () => _cellObj.CommitAction(sender, this, _context, action));
    }
}