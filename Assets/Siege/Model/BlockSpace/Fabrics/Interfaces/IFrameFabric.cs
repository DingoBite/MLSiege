using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.Blocks.Abstracts;
using UnityEngine;

namespace Assets.Siege.Model.BlockSpace.Fabrics.Interfaces
{
    public interface IFrameFabric<TFrame, TInfo, TMono> where TMono: BlockSpaceMonoObject<TInfo>
    {
        public FrameBlock Make(Vector3Int coords, TInfo blockInfo, IBlockSpace<TFrame, TInfo, TMono> blockSpace);
        public FrameBlock Make(TMono block, IBlockSpace<TFrame, TInfo, TMono> blockSpace);
    }
}