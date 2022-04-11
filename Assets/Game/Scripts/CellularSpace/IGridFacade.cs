using Game.Scripts.General.FlexibleDataApi;
using UnityEngine;

namespace Game.Scripts.CellularSpace
{
    public interface IGridFacade
    {
        void Init(Grid grid, Grid gameGrid);
        void CommitSelectAction(int id);
        void CommitAction(int id, PerformanceParams performanceData);
        void CommitActionToSelected(PerformanceParams performanceData);
    }
}