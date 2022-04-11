using System;
using Game.Scripts.CellularSpace.CellStorages.CellObjects;
using Game.Scripts.General.FlexibleDataApi;
using UnityEngine;

namespace Game.Scripts.View.CellObjects.Serialization.Interfaces
{
    public class MonoBlock : MonoSoloCellObject
    {
        protected override AbstractChildCellObject MakeCellObject(int id, 
            Action<object, PerformanceParams> commitReaction, bool isExternallyModifiable)
        {
            return new CellBlock(id, commitReaction, isExternallyModifiable);
        }
    }
}