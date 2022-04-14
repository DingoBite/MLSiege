﻿using System;
using Game.Scripts.CellularSpace.CellStorages.CellObjects;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Realizations;
using Game.Scripts.General.FlexibleDataApi;

namespace Game.Scripts.View.CellObjects.Serialization
{
    public class MonoBlock : MonoSoloCellObject
    {
        protected override AbstractChildCellObject MakeCellObject(int id, 
            Action<object, PerformanceParam> commitReaction, bool isExternallyModifiable)
        {
            return new CellBlock(id, commitReaction, isExternallyModifiable);
        }
    }
}