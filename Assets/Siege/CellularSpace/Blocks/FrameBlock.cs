﻿using Assets.Siege.CellularSpace.Agents;
using Assets.Siege.CellularSpace.Agents.Enums;
using Assets.Siege.CellularSpace.Blocks.Enums;
using Assets.Siege.CellularSpace.Features;
using Assets.Siege.CellularSpace.General.CellObjects;
using Assets.Siege.CellularSpace.Repositories.Interfaces;
using Assets.Siege.View.Blocks;

namespace Assets.Siege.CellularSpace.Blocks
{
    public class FrameBlock: CellFrame<FrameBlock, Block, MonoBlock, BlockFeatures>
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