using System;
using Game.Scripts.CellularSpace.CellStorages.CellObjects;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.CellularSpace.GridShape.CoordsConverters.Interfaces;
using Game.Scripts.CellularSpace.GridShape.Interfaces;
using Game.Scripts.General.FlexibleDataApi;
using UnityEngine;
using Zenject;

namespace Game.Scripts.CellularSpace
{
    public class GridFacade : IGridFacade
    {
        private IGridLevelsManager _gridLevelsManager;
        private IGridCoordsConverter _gridCoordsConverter;
        private ICellGrid _cellGrid;
        private int _selectedCellObjectId;
        
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

        public void Init(Grid grid)
        {
            _gridLevelsManager.Init(grid);
            _gridCoordsConverter.Init(_gridLevelsManager.CellSize);
            _cellGrid.Init(_gridLevelsManager, _gridCoordsConverter);
        }

        public void CommitAction(Vector3 position)
        {
            if (!_cellGrid.TryGetCell(_gridCoordsConverter.Convert(position), out var cell)) 
                return;
            if (_cellGrid.TryGetCellObject(_selectedCellObjectId, out var selectedCell))
                selectedCell.CommitAction(new ActionPerformanceData<CellBlockLogicAction>(CellBlockLogicAction.Unselect));
            
            cell.CellObject.CommitAction(new ActionPerformanceData<CellBlockLogicAction>(CellBlockLogicAction.Select));
            _selectedCellObjectId = cell.CellObject.Id;
        }
        
        public void CommitAction(Vector3 position, FlexibleData actionPerformanceData)
        {
            if (actionPerformanceData == null) 
                throw new ArgumentNullException(nameof(actionPerformanceData));
            if (!_cellGrid.TryGetCell(_gridCoordsConverter.Convert(position), out var cell)) 
                return;
            
            cell.CellObject.CommitAction(actionPerformanceData);
        }

        public void CommitAction(FlexibleData actionPerformanceData)
        {
            if (_cellGrid.TryGetCellObject(_selectedCellObjectId, out var selectedCell))
                selectedCell.CommitAction(actionPerformanceData);
        }
    }
}