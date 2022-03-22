using System.Collections.Generic;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.CellularSpace.GridShape.CoordsConverters.Interfaces;
using Game.Scripts.CellularSpace.GridShape.Interfaces;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages
{
    public interface ICellGrid
    {
        void Init(IGridLevelsManager gridLevelsManager, IGridCoordsConverter gridCoordsConverter);
        bool TryGetCell(Vector3Int coords, out ICell cell);
        IEnumerable<ICell> GetCells();
    }
}