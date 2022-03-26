using System;
using System.Linq;
using Game.Scripts.CellularSpace.CellStorages.CellObjects;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.CellularSpace.GridShape.CoordsConverters.Interfaces;
using Game.Scripts.CellularSpace.GridShape.Interfaces;
using Game.Scripts.General.FlexibleDataApi;
using Game.Scripts.Time;
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

        [Inject] private TimePeriodTicker _timePeriodTicker;
        
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
            _timePeriodTicker.SecondsTimePeriod = 1;
            _timePeriodTicker.AddUpdatable(this);
        }

        public void CommitSelectAction(Vector3 position)
        {
            if (!_cellGrid.TryGetCell(_gridCoordsConverter.Convert(position), out var cell)) 
                return;
            if (_cellGrid.TryGetCellObject(_selectedCellObjectId, out var selectedCell))
                selectedCell.CommitAction(this, new ActionPerformanceParams<CellBlockAction>(CellBlockAction.Unselect));
            
            cell.CellObject.CommitAction(this, new ActionPerformanceParams<CellBlockAction>(CellBlockAction.Select));
            _selectedCellObjectId = cell.CellObject.Id;
        }
        
        public void CommitAction(Vector3 position, PerformanceParams performanceData)
        {
            if (performanceData == null) 
                throw new ArgumentNullException(nameof(performanceData));
            if (!_cellGrid.TryGetCell(_gridCoordsConverter.Convert(position), out var cell)) 
                return;
            
            cell.CellObject.CommitAction(this, performanceData);
        }

        public void CommitAction(PerformanceParams performanceData)
        {
            if (_cellGrid.TryGetCellObject(_selectedCellObjectId, out var selectedCell))
                selectedCell.CommitAction(this, performanceData);
        }

        public void OnUpdate()
        {
            var gravityAct = new ActionPerformanceParams<CellObjectBaseAction>(CellObjectBaseAction.ApplyGravity);
            foreach (var cell in _cellGrid.GetCells().Where(c => !c.IsEmpty && c.CellObject.IsIndependent))
            {
                cell.CellObject.CommitAction(this, gravityAct);
            }
        }
    }
}