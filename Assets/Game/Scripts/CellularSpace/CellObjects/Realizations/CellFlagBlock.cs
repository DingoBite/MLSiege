using System;
using Game.Scripts.CellularSpace.CellObjects.CellObjectCharacteristics;
using Game.Scripts.CellularSpace.CellObjects.CellObjectCharacteristics.Interfaces;
using Game.Scripts.CellularSpace.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellObjects.Enums.Block;
using Game.Scripts.General.FlexibleDataApi;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellObjects.Realizations
{
    public class CellFlagBlock : AbstractChildCellObject
    {
        private readonly Action _endGameAction;

        private readonly FlagCharacteristic _characteristic;
        
        public CellFlagBlock(int id, FlagCharacteristic characteristics,
            Action<object, PerformanceParam> commitReaction, Action endGameAction, bool isModifiable)
            : base(id, commitReaction, isModifiable)
        {
            _characteristic = characteristics;
            _endGameAction = endGameAction;
        }
        
        private void Destroy()
        {
            _commitReaction.Invoke(this, CellBlockViewActions.Destroy);
            ParentCellMutable?.Clear();
            _endGameAction?.Invoke();
        }

        public override ICharacteristics Characteristics => _characteristic;
        public override bool CommitAction(object sender, PerformanceParam performanceParam)
        {
            if (!(performanceParam.EnumActionType is CellBlockAction cellBlockBaseAction)) return false;
            if (cellBlockBaseAction != CellBlockAction.GetHit) return false;
            Destroy();
            return true;
        }
    }
}