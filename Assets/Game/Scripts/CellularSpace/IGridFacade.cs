using System.Collections.Generic;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.CellularSpace.GridStep;
using Game.Scripts.General.FlexibleDataApi;
using UnityEngine;

namespace Game.Scripts.CellularSpace
{
    public interface IGridFacade
    {
        void Init(Grid grid, Grid gameGrid);
        void ReInit(Grid gameGrid);
        bool CommitSelectAction(int id);
        bool CommitAction(int id, PerformanceParam performanceData);
        bool CommitActionToSelected(PerformanceParam performanceData);
        IEnumerable<(ICell, StepData)> SelectedPathFind(int targetId);
        IEnumerable<(ICell, StepData)> PathFind(int id, int targetId);
        bool TryGetCoordsFromId(int id, out Vector3Int coords);
        int GetIdFromCoords(Vector3Int coords);
        int? GetRelativeValue(Vector3Int senderCoords, Vector3Int targetCoords);
        IEnumerable<int> GetAgentIds();
        IEnumerable<int> GetGoalIds();
        IEnumerable<int> GetBlockIds();
    }
}