using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.GridShapers.Interfaces;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.Blocks;
using UnityEngine;
using Zenject;

namespace Assets.Siege.Model.BlockSpace.GridShapers
{
    public class BlockGridShaper: IGridShaper<BlockInfo, MonoBlock>
    {
        private readonly ITilemapLevelsGrid _tilemapLevelsGrid;

        public BlockGridShaper(Grid grid, [Inject] ITilemapLevelsGrid tilemapLevelsGrid)
        {
            _tilemapLevelsGrid = tilemapLevelsGrid;
            _tilemapLevelsGrid.Init(grid);
        }

        public (Vector3, Vector3) Shape(IBlockSpaceController<BlockInfo, MonoBlock> blockSpace)
        {
            var minX = float.MaxValue;
            var minY = float.MaxValue;
            var minZ = float.MaxValue;
            var maxX = float.MinValue;
            var maxY = float.MinValue;
            var maxZ = float.MinValue;

            foreach (var tilemap in _tilemapLevelsGrid.GetLevels())
            {
                foreach (Transform block in tilemap.transform)
                {
                    var isBlock = block.TryGetComponent(out MonoBlock monoBlock);
                    if (!isBlock) continue;
                    blockSpace.InsertBlock(monoBlock);
                    var pos = block.position;
                    if (pos.x < minX) minX = pos.x;
                    if (pos.y < minY) minY = pos.y;
                    if (pos.z < minZ) minZ = pos.z;
                    if (pos.x > maxX) maxX = pos.x;
                    if (pos.y > maxY) maxY = pos.y;
                    if (pos.z > maxZ) maxZ = pos.z;
                }
            }

            return (new Vector3(minX, minY, minZ), new Vector3(maxX, maxY, maxZ));
        }
    }
}