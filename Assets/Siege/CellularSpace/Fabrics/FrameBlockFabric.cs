using Assets.Siege.CellularSpace.Blocks;
using Assets.Siege.CellularSpace.Fabrics.Interfaces;
using Assets.Siege.CellularSpace.GridShapers.Interfaces;
using Assets.Siege.CellularSpace.Repositories.Interfaces;
using Assets.Siege.View.Blocks;
using UnityEngine;
using Zenject;

namespace Assets.Siege.CellularSpace.Fabrics
{
    public class FrameBlockFabric: IFrameFabric<FrameBlock, BlockInfo, MonoBlock>
    {
        private readonly IMonoFabric<MonoBlock, Block> _monoFabric;

        [Inject]
        public FrameBlockFabric(IMonoFabric<MonoBlock, Block> monoFabric)
        {
            _monoFabric = monoFabric;
        }

        public void Init(ITilemapLevelsGrid<MonoBlock> tilemapLevelsGrid) => _monoFabric.Init(tilemapLevelsGrid);

        public FrameBlock Make(Vector3Int coords, BlockInfo blockInfo, IFrameSpaceContext<FrameBlock> space)
        {
            var id = space.PeekId;
            var block = new Block(blockInfo);
            var position = space.Convert(coords);
            var monoBlock = _monoFabric.Make(id, coords.y, position, block);
            return new FrameBlock(space, block, monoBlock);
        }

        public FrameBlock Make(MonoBlock monoAgent, IFrameSpaceContext<FrameBlock> space)
        {
            monoAgent.Id = space.PeekId;
            monoAgent.name = monoAgent.ToString();
            var block = new Block(monoAgent.GetInfo());
            return new FrameBlock(space, block, monoAgent);
        }
    }
}