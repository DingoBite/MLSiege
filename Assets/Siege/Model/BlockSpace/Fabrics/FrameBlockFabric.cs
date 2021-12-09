using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.Fabrics.Interfaces;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.Blocks;
using UnityEngine;
using Zenject;

namespace Assets.Siege.Model.BlockSpace.Fabrics
{
    public class FrameBlockFabric: IFrameFabric<FrameBlock, BlockInfo, MonoBlock>
    {
        private readonly IMonoFabric<MonoBlock> _monoFabric;

        [Inject]
        public FrameBlockFabric(
            IMonoFabric<MonoBlock> monoFabric)
        {
            _monoFabric = monoFabric;
        }

        public FrameBlock Make(Vector3Int coords, BlockInfo blockInfo, IBlockSpace<FrameBlock, BlockInfo, MonoBlock> blockSpace)
        {
            var id = blockSpace.PeekId();
            var block = new Block(blockInfo);
            var position = blockSpace.Convert(coords);
            var monoBlock = _monoFabric.Make(id, coords.y, position, block);
            return new FrameBlock(blockSpace, block, monoBlock, coords);
        }

        public FrameBlock Make(MonoBlock monoBlock, IBlockSpace<FrameBlock, BlockInfo, MonoBlock> blockSpace)
        {
            monoBlock.name = monoBlock.ToString();
            var coords = blockSpace.Convert(monoBlock.transform.position);
            var block = new Block(monoBlock.GetInfo());
            return new FrameBlock(blockSpace, block, monoBlock, coords);
        }
    }
}