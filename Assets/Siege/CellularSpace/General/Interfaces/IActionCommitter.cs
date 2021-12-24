using Assets.Siege.CellularSpace.Repositories.Interfaces;

namespace Assets.Siege.CellularSpace.General.Interfaces
{
    public interface IActionCommitter<TSender, TCommitter, in TAction>
    {
        public void CommitAction(TSender sender, TCommitter committer, 
            IFrameSpaceInfo<TSender> senderSpace,
            IFrameSpaceInfo<TCommitter> committerSpace, 
            TAction action);
    }
}