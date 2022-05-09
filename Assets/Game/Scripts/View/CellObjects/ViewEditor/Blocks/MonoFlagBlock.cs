using System;
using Game.Scripts.CellObjects;
using Game.Scripts.CellObjects.CellObjectCharacteristics;
using Game.Scripts.CellObjects.Enums;
using Game.Scripts.CellObjects.Realizations;
using Game.Scripts.General.FlexibleDataApi;

namespace Game.Scripts.View.CellObjects.ViewEditor.Blocks
{
    public class MonoFlagBlock : MonoCellObject
    {
        protected override bool IsModifiable => true;
        protected override CellObjectType CellObjectType => CellObjectType.Flag;
        private FlagCharacteristic Characteristics => new FlagCharacteristic();
        protected override AbstractChildCellObject MakeCellObject(int id, 
            Action<object, PerformanceParam> commitReaction, bool isExternallyModifiable)
        {
            return new CellFlagBlock(id, Characteristics, commitReaction,
                () => { }, isExternallyModifiable);
        }
    }
}