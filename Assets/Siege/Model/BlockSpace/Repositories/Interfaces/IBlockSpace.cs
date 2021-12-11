using Assets.Siege.View.General.MonoBehaviors;

namespace Assets.Siege.Model.BlockSpace.Repositories.Interfaces
{
    public interface IBlockSpace<TFrame, in TInfo, in TMono> : IFrameSpaceContext<TFrame>, IBlockSpaceController<TInfo, TMono>
        where TMono : BlockSpaceMonoObject<TInfo>
    {
    }
}