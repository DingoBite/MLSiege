using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.MonoBehaviors.CellableObjects;
using UnityEngine;

namespace Assets.Siege.Model.CellularSpace.Shapers
{
    public class LevelGridShaper: IGridShaper
    {
        public LevelGridShaper() { }
        public void Shape(IGameObjectGrid gameObjectGrid, IBlockSpace blockSpace)
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