using Assets.Siege.Model.BlockSpace.Agents;
using Assets.Siege.Model.BlockSpace.Agents.Enums;
using Assets.Siege.Model.BlockSpace.Blocks.Enums;
using Assets.Siege.Model.BlockSpace.Features;
using Assets.Siege.Model.BlockSpace.General.CellObjects;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.Blocks;

namespace Assets.Siege.Model.BlockSpace.Blocks
{
    public class FrameBlock: CellObjectFrame<FrameBlock, Block, MonoBlock, BlockFeatures>
    {
        public FrameBlock(IFrameSpaceContext<FrameBlock> selfSpace, Block block, MonoBlock monoBlock)
            : base(selfSpace, block, monoBlock)
        {
        }

        public void CommitAction(FrameAgent sender, AgentAction action) 
            => _mono.Act(action, () => _cellObj.CommitAction(sender, this, sender.SelfSpace, SelfSpace, action));

        public void CommitAction(FrameBlock sender, BlockAction action) 
            => _mono.Act(action, () => _cellObj.CommitAction(sender, this, sender.SelfSpace, SelfSpace, action));

    }
}