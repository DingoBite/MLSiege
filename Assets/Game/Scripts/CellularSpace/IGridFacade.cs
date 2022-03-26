using Game.Scripts.General.FlexibleDataApi;
using UnityEngine;

namespace Game.Scripts.CellularSpace
{
    public interface IGridFacade
    {
        void Init(Grid grid);
        void CommitSelectAction(Vector3 position);
        void CommitAction(Vector3 position, PerformanceParams performanceData);
        void CommitAction(PerformanceParams performanceData);
    }
}