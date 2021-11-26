using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.MonoBehaviors.CellableObjects;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Siege.Model.CellularSpace.Shapers
{
    public class GridShaper: IGridShaper
    {
        public GridShaper() { }
        public void Shape(IGridHierarchy gridHierarchy, IBlockSpace blockSpace)
        {
            foreach (Transform tilemap in gridHierarchy.GetChildren())
            {
                var containsTilemap = tilemap.TryGetComponent(out Tilemap _);
                if (!containsTilemap) continue;

                foreach (Transform block in tilemap)
                {
                    var isBlock = block.TryGetComponent(out MonoBlock monoBlock);
                    if (!isBlock) continue;
                    blockSpace.InsertBlock(monoBlock);
                }
            }
        }
    }
}