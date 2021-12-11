using Assets.Siege.Model.BlockSpace.Agents;
using Assets.Siege.Model.BlockSpace.Agents.Enums;
using Assets.Siege.Model.BlockSpace.Blocks.Enums;
using Assets.Siege.Model.BlockSpace.General;

namespace Assets.Siege.Model.BlockSpace.Blocks
{
    public readonly struct BlockData
    {
        public readonly BlockType BlockType;
        public readonly BlockSolidity BlockSolidity;

        public BlockData(BlockType blockType, BlockSolidity blockSolidity)
        {
            BlockType = blockType;
            BlockSolidity = blockSolidity;
        }
    }

    public class BlockInfo
    {
        public readonly BlockData BlockData;
        public readonly BlockObjectBehavior<FrameAgent, FrameBlock, AgentAction> AgentToBlockBehavior;
        public readonly BlockObjectBehavior<FrameBlock, FrameBlock, BlockAction> BlockToBlockBehavior;

        public BlockInfo(BlockData blockData,
            BlockObjectBehavior<FrameAgent, FrameBlock, AgentAction> agentToBlockBehavior,
            BlockObjectBehavior<FrameBlock, FrameBlock, BlockAction> blockToBlockBehavior
            )
        {
            BlockData = blockData;
            AgentToBlockBehavior = agentToBlockBehavior;
            BlockToBlockBehavior = blockToBlockBehavior;
        }

        public BlockType BlockType => BlockData.BlockType;
        public BlockSolidity BlockSolidity => BlockData.BlockSolidity;
    }
}