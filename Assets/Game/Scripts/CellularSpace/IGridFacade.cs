using Game.Scripts.General.FlexibleDataApi;
using UnityEngine;

namespace Game.Scripts.CellularSpace
{
    public interface IGridFacade
    {
        void Init(Grid grid);
        void CommitAction(Vector3 position);
        void CommitAction(Vector3 position, FlexibleData actionPerformanceData);
        void CommitAction(FlexibleData actionPerformanceData);
    }
}