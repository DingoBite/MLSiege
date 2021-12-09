using Assets.Siege.Model.BlockSpace.Agents;
using Assets.Siege.Model.BlockSpace.Agents.Enums;
using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.Blocks.Enums;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.Blocks.Abstracts;
using UnityEngine;

namespace Assets.Siege.View.Blocks.Realizations
{
    [CreateAssetMenu(fileName = "Flag", menuName = "ScriptableObjects/Blocks/Flag")]
    public class Flag: ScriptableBlock
    {
        public override BlockInfo GetInfo() => new BlockInfo(new BlockData(BlockType.Flag, BlockSolidity.Unobstructed), CommitAction, null);

        protected override void CommitAction(FrameAgent sender, FrameBlock committer,
            IFrameSpaceContext<FrameAgent> senderSpace,
            IFrameSpaceContext<FrameBlock> frameSpace, AgentAction blockAction)
            => committer.Features.CommitFeatureChange(0, 1);
    }
}