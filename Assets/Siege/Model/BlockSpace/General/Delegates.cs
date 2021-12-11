using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;

namespace Assets.Siege.Model.BlockSpace.General
{
    public delegate void BlockObjectBehavior<TSender, TCommitter, in TAction>
        (TSender sender, TCommitter committer, IFrameSpaceContext<TSender> senderSpace, IFrameSpaceContext<TCommitter> committerSpace, TAction action);

}