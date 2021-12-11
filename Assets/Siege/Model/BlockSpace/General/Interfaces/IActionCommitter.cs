using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;

namespace Assets.Siege.Model.BlockSpace.General.Interfaces
{
    public interface IActionCommitter<TSender, TCommitter, in TAction>
    {
        public void CommitAction(TSender sender, TCommitter committer, 
            IFrameSpaceContext<TSender> senderSpace,
            IFrameSpaceContext<TCommitter> committerSpace, 
            TAction action);
    }
}