using System;
using System.Linq;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.CellularSpace.GridShape.CoordsConverters.Interfaces;
using Game.Scripts.CellularSpace.GridShape.Interfaces;
using Game.Scripts.General.FlexibleDataApi;
using Game.Scripts.Time.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Scripts.CellularSpace
{
    public class GridFacade : IGridFacade, IUpdatable
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

        public void CommitSelectAction(int id)
        {
            if (!_cellGrid.TryGetCellObject(id, out var cellObject)) 
                return;
            if (!cellObject.IsExternallyModifiable) 
                return;
            if (_cellGrid.TryGetCellObject(_selectedCellObjectId, out var selectedCellObject))
                selectedCellObject.CommitAction(this, new ActionPerformanceParams<CellObjectBaseAction>(CellObjectBaseAction.Unselect));
            
            cellObject.CommitAction(this, new ActionPerformanceParams<CellObjectBaseAction>(CellObjectBaseAction.Select));
            _selectedCellObjectId = cellObject.Id;
        }
        
        public void CommitAction(int id, PerformanceParams performanceData)
        {
            if (performanceData == null) 
                throw new ArgumentNullException(nameof(performanceData));
            if (!_cellGrid.TryGetCellObject(id, out var cellObject)) 
                return;
            if (!cellObject.IsExternallyModifiable) 
                return;
            
            cellObject.CommitAction(this, performanceData);
        }

        public void CommitActionToSelected(PerformanceParams performanceData)
        {
            if (!_cellGrid.TryGetCellObject(_selectedCellObjectId, out var selectedCellObject))
                return;
            if (!selectedCellObject.IsExternallyModifiable) 
                return;
            selectedCellObject.CommitAction(this, performanceData);
        }

        public void OnUpdate()
        {
            var gravityAct = new ActionPerformanceParams<CellObjectBaseAction>(CellObjectBaseAction.ApplyGravity);
            foreach (var cell in _cellGrid.GetCells().Where(c => !c.IsEmpty && c.CellObject.IsExternallyModifiable))
            {
                cell.CellObject.CommitAction(this, gravityAct);
            }
        }
    }
}