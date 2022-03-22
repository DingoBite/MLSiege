using System.Collections.Generic;
using Game.Scripts.View.CellObjects;
using Game.Scripts.View.GridLevels;
using UnityEngine;

namespace Game.Scripts.CellularSpace.GridShape.Interfaces
{
    public interface IGridLevelsManager
    {
        void Init(Grid grid);
        Vector3 CellSize { get; }
        int LevelsCount { get; }
        AbstractMonoCellObject PutIntoLevel(int level, AbstractMonoCellObject prefab, Vector3 position, int id);
        IEnumerable<GridLevel> GetLevels();
    }
}