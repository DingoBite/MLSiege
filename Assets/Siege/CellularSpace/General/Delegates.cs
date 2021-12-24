using Assets.Siege.CellularSpace.Repositories.Interfaces;

namespace Assets.Siege.CellularSpace.General
{
    public delegate void BlockObjectBehavior<TSender, TCommitter, in TAction>
    (TSender sender, TCommitter committer, IFrameSpaceInfo<TSender> senderSpace, IFrameSpaceInfo<TCommitter> committerSpace, TAction action);

}