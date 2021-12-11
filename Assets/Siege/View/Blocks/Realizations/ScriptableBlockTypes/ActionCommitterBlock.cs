using Assets.Siege.Model.BlockSpace.Agents;
using Assets.Siege.Model.BlockSpace.Agents.Enums;
using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.Blocks.Enums;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;

namespace Assets.Siege.View.Blocks.Realizations.ScriptableBlockTypes
{
    public abstract class ActionCommitterBlock: CommonBlock
    {
        protected abstract void CommitAction(FrameAgent sender, FrameBlock committer,
            IFrameSpaceContext<FrameAgent> senderSpace, IFrameSpaceContext<FrameBlock> committerSpace,
            AgentAction action);
        protected abstract void CommitAction(FrameBlock sender, FrameBlock committer,
            IFrameSpaceContext<FrameBlock> senderSpace, IFrameSpaceContext<FrameBlock> committerSpace,
            BlockAction action);
    }
}