using Game.Scripts.General.FlexibleDataApi;
using UnityEngine;

namespace Game.Scripts.CellularSpace
{
    public interface IGridFacade
    {
        void Init(Grid grid, Grid gameGrid);
        void CommitSelectAction(int id);
        void CommitAction(int id, PerformanceParam performanceData);
        void CommitActionToSelected(PerformanceParam performanceData);
        void CommitSelectedPathFind(int targetId);
        void ApplyGlobalAction();
    }
}