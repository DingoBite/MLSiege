﻿using System;
 using Game.Scripts.CellularSpace.CellObjects;
 using Game.Scripts.CellularSpace.CellObjects.CellObjectCharacteristics;
 using Game.Scripts.CellularSpace.CellObjects.Enums;
 using Game.Scripts.CellularSpace.CellObjects.Realizations;
 using Game.Scripts.General.FlexibleDataApi;

namespace Game.Scripts.View.CellObjects.Serialization
{
    public abstract class MonoBlock : MonoCellObject
    {
        protected abstract BlockCharacteristic Characteristics { get; }
        protected override CellObjectType CellObjectType => CellObjectType.Block;
        protected override AbstractChildCellObject MakeCellObject(int id, 
            Action<object, PerformanceParam> commitReaction, bool isExternallyModifiable)
        {
            return new CellBlock(id, Characteristics, commitReaction, isExternallyModifiable);
        }
    }
}