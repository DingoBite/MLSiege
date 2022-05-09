using System;
using Game.Scripts.CellObjects;
using Game.Scripts.CellObjects.CellObjectCharacteristics;
using Game.Scripts.CellObjects.Enums;
using Game.Scripts.CellObjects.Realizations;
using Game.Scripts.General.FlexibleDataApi;

namespace Game.Scripts.View.CellObjects.ViewEditor
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