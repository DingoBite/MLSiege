using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.CellularSpace.GridShape.CoordsConverters.Interfaces;
using Game.Scripts.CellularSpace.GridShape.Interfaces;
using Game.Scripts.General.FlexibleDataApi;
using Game.Scripts.PathFind;
using UnityEngine;
using Zenject;

namespace Game.Scripts.CellularSpace
{
    public class GridFacade : IGridFacade
    {
        private readonly IGridLevelsManager _gridLevelsManager;
        private readonly IGridCoordsConverter _gridCoordsConverter;
        private readonly ICellGrid _cellGrid;
        private int _selectedCellObjectId = -1;

        [Inject]
        public GridFacade(
            IGridLevelsManager gridLevelsManager,
            IGridCoordsConverter gridCoordsConverter,
            ICellGrid cellGrid)
        {
            _gridLevelsManager = gridLevelsManager;
            _gridCoordsConverter = gridCoordsConverter;
            _cellGrid = cellGrid;
        }

        public void Init(Grid grid, Grid gameGrid)
        {
            _gridLevelsManager.Init(grid);
            _gridCoordsConverter.Init(_gridLevelsManager.CellSize);
            _cellGrid.Init(_gridLevelsManager, _gridCoordsConverter, gameGrid);
        }

        public void ReInit(Grid gameGrid)
        {
            _cellGrid.Init(_gridLevelsManager, _gridCoordsConverter, gameGrid);
        }

        public bool CommitSelectAction(int id)
        {
            if (!_cellGrid.TryGetCellObject(id, out var cellObject)) 
                return false;
            if (!cellObject.IsModifiable) 
                return false;
            if (_cellGrid.TryGetCellObject(_selectedCellObjectId, out var selectedCellObject))
                selectedCellObject.CommitAction(this, CellObjectBaseActions.Unselect);

            if (!cellObject.CommitAction(this, CellObjectBaseActions.Select))
                return false;
            _selectedCellObjectId = cellObject.Id;
            return true;
        }
        
        public bool CommitAction(int id, PerformanceParam performanceData)
        {
            if (performanceData == null) 
                throw new ArgumentNullException(nameof(performanceData));
            if (!_cellGrid.TryGetCellObject(id, out var cellObject)) 
                return false;
            if (!cellObject.IsModifiable) 
                return false;
            
            return cellObject.CommitAction(this, performanceData);
        }

        public bool CommitActionToSelected(PerformanceParam performanceData)
        {
            if (!_cellGrid.TryGetCellObject(_selectedCellObjectId, out var selectedCellObject))
                return false;
            if (!selectedCellObject.IsModifiable) 
                return false;
            return selectedCellObject.CommitAction(this, performanceData);
        }

        public IEnumerable<(ICell, StepData)> SelectedPathFind(int targetId) =>
            PathFind(_selectedCellObjectId, targetId);

        public IEnumerable<(ICell, StepData)> PathFind(int id, int targetId)
        {
            if (!_cellGrid.TryGetCellObject(id, out var agent))
                return null;
            if (agent.CellObjectType != CellObjectType.Agent || !agent.IsModifiable) 
                return null;
            if (!_cellGrid.TryGetCellObject(targetId, out var target))
                return null;
            return _cellGrid.FindPath(agent, target.ParentCell);
        }

        public bool TryGetCoordsFromId(int id, out Vector3Int coords)
        {
            if (!_cellGrid.TryGetCellObject(id, out var cellObject))
            {
                coords = Vector3Int.zero;
                return false;
            }
            coords = cellObject.Coords;
            return true;
        }

        public int GetIdFromCoords(Vector3Int coords)
        {
            if (!_cellGrid.TryGetCell(coords, out var cell)) return -2;
            if (cell.IsEmpty) return -1;
            return cell.CellObject.Id;
        }

        public int GetRelativeValue(Vector3Int senderCoords, Vector3Int targetCoords)
        {
            if (!_cellGrid.TryGetCell(senderCoords, out var senderCell))
                throw new ArgumentException($"Can't find cell with {nameof(senderCoords)}");
            if (senderCell.IsEmpty)
                throw new ArgumentException($"Can't find sender on {nameof(senderCoords)}");
            if (!_cellGrid.TryGetCell(targetCoords, out var targetCell))
                return senderCell.CellObject.EvaluateCell(null);
            return senderCell.CellObject.EvaluateCell(targetCell);
        }


        public IEnumerable<int> GetAgentIds() =>
            _cellGrid.GetCells()
                .AsParallel()
                .Where(c => !c.IsEmpty && c.CellObject.CellObjectType == CellObjectType.Agent)
                .Select(c => c.CellObject.Id);

        public IEnumerable<int> GetGoalIds() =>
            _cellGrid.GetCells()
                .AsParallel()
                .Where(c => !c.IsEmpty && c.CellObject.CellObjectType == CellObjectType.Flag)
                .Select(c => c.CellObject.Id);
        

        public IEnumerable<int> GetBlockIds() =>
            _cellGrid.GetCells()
                .AsParallel()
                .Where(c => !c.IsEmpty && c.CellObject.CellObjectType == CellObjectType.Block)
                .Select(c => c.CellObject.Id);
    }
}