using Assets.Siege.CellularSpace.Repositories.Interfaces;
using Assets.Siege.View.General.MonoBehaviors;
using UnityEngine;

namespace Assets.Siege.CellularSpace.GridShapers.Interfaces
{
    public interface IGridShaper<out TInfo, TMono> where TMono : CellularSpaceMonoObject<TInfo>
    {
        public void Init(ITilemapLevelsGrid<TMono> tilemapLevelsGrid);
        public (Vector3, Vector3) Shape(IFrameSpaceDataController<TInfo, TMono> frameSpace);
    }
}