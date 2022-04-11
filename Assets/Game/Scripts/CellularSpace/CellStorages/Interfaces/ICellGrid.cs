using Game.Scripts.CellularSpace.GridShape.CoordsConverters.Interfaces;
using Game.Scripts.CellularSpace.GridShape.Interfaces;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages.Interfaces
{
    public interface ICellGrid : ICellGridContext, ICellGridEditor
    {
        void Init(IGridLevelsManager gridLevelsManager, IGridCoordsConverter gridCoordsConverter, Grid gameGrid);
    }
}