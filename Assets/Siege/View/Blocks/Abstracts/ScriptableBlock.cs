using Assets.Siege.Model.BlockSpace.Agents;
using Assets.Siege.Model.BlockSpace.Agents.Enums;
using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.General;

namespace Assets.Siege.View.Blocks.Abstracts
{
    public abstract class ScriptableBlock: InfoScriptableObject<BlockInfo>
    {
        protected abstract void CommitAction(FrameAgent sender, FrameBlock committer,
            IFrameSpaceContext<FrameAgent> senderSpace, IFrameSpaceContext<FrameBlock> frameSpace,
            AgentAction blockAction);
    }
}