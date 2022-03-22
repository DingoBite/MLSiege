using Game.Scripts.CellularSpace.CellStorages.CellObjects;
using UnityEngine;

namespace Game.Scripts.CellularSpace.GridShape
{
    public interface IGridFacade
    {
        void Init(Grid grid);
        FlexibleData CommitAction(Vector3 position);
    }
}