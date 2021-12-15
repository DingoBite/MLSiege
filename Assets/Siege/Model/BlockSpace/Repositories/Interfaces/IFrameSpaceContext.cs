using Assets.Siege.Model.BlockSpace.Features;

namespace Assets.Siege.Model.BlockSpace.Repositories.Interfaces
{
    public interface IFrameSpaceContext<TFrame> : IFrameSpaceMover, IFrameSpaceInfo<TFrame>
    {
        
    }
}