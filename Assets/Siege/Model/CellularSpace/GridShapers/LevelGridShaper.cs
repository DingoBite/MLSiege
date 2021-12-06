using Assets.Siege.Model.CellularSpace.GridShapers.Interfaces;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.View.Blocks;
using UnityEngine;

namespace Assets.Siege.Model.CellularSpace.GridShapers
{
    public class LevelGridShaper: IGridShaper
    {
        public void Shape(IGameObjectGrid gameObjectGrid, IBlockSpaceController blockSpace)
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