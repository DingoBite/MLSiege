using System.Collections.Generic;
using Game.Scripts.CellularSpace.GridShape.CoordsConverters.Interfaces;
using Game.Scripts.CellularSpace.GridShape.Interfaces;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages.Interfaces
{
    public interface ICellGrid
    {
        void Init(IGridLevelsManager gridLevelsManager, IGridCoordsConverter gridCoordsConverter);
        bool TryGetCell(Vector3Int coords, out ICell cell);
        void ClearDisposed();
        IEnumerable<ICell> GetCells();
    }
}