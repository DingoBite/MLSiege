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
        private readonly IMonoFabric<MonoBlock, Block> _monoFabric;

        [Inject]
        public FrameBlockFabric(IMonoFabric<MonoBlock, Block> monoFabric)
        {
            _monoFabric = monoFabric;
        }

        public FrameBlock Make(Vector3Int coords, BlockInfo blockInfo, IBlockSpace<FrameBlock, BlockInfo, MonoBlock> space)
        {
            var id = space.PeekId;
            var block = new Block(blockInfo);
            var position = space.Convert(coords);
            var monoBlock = _monoFabric.Make(id, coords.y, position, block);
            return new FrameBlock(space, block, monoBlock, coords);
        }

        public FrameBlock Make(MonoBlock mono, IBlockSpace<FrameBlock, BlockInfo, MonoBlock> space)
        {
            mono.name = mono.ToString();
            mono.Id = space.PeekId;
            var coords = space.Convert(mono.transform.position);
            var block = new Block(mono.GetInfo());
            return new FrameBlock(space, block, mono, coords);
        }
    }
}