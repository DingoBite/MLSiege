using Assets.Siege.View.General.MonoBehaviors;

namespace Assets.Siege.Model.BlockSpace.Repositories.Interfaces
{
    public interface IFrameSpace<TFrame, in TInfo, in TMono> : IFrameSpaceContext<TFrame>, IFrameSpaceDataController<TInfo, TMono>
        where TMono : FrameSpaceMonoObject<TInfo>
    {
    }
}