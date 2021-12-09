using Assets.Siege.Model.BlockSpace.Agents;
using Assets.Siege.Model.BlockSpace.Agents.Enums;
using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.Blocks.Enums;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.Blocks.Realizations.ScriptableBlockTypes;
using UnityEngine;

namespace Assets.Siege.View.Blocks.Realizations
{
    [CreateAssetMenu(fileName = "Dirt", menuName = "ScriptableObjects/Blocks/DestructibleBlock/Dirt")]
    public class Dirt: DestructibleBlock
    {
        public override BlockInfo GetInfo() 
            => new BlockInfo(new BlockData(BlockType.Dirt, BlockSolidity.Solid), CommitAction, null);

        protected override void CommitAction(FrameAgent sender, FrameBlock committer, 
            IFrameSpaceContext<FrameAgent> senderSpace, IFrameSpaceContext<FrameBlock> committerSpace, AgentAction blockAction)
        {
        }
    }
}