using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.GridShapers.Interfaces;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.Blocks;
using UnityEngine;

namespace Assets.Siege.Model.BlockSpace.GridShapers
{
    public class LevelGridShaper: IGridShaper<BlockInfo, MonoBlock>
    {
        public void Shape(IGameObjectGrid gameObjectGrid, IBlockSpaceController<BlockInfo, MonoBlock> blockSpace)
        {
            foreach (var tilemap in gameObjectGrid.GetLevels())
            {
                foreach (Transform block in tilemap.transform)
                {
                    var isBlock = block.TryGetComponent(out MonoBlock monoBlock);
                    if (!isBlock) continue;
                    blockSpace.InsertBlock(monoBlock);
                }
            }
        }
    }
}