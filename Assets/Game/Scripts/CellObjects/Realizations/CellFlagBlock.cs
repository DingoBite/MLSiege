using System;
using Game.Scripts.CellularSpace.CellObjects.CellObjectCharacteristics;
using Game.Scripts.CellularSpace.CellObjects.CellObjectCharacteristics.Interfaces;
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
            ParentCell?.Clear();
            _endGameAction?.Invoke();
        }

        public override ICharacteristics Characteristics => _characteristic;
        public override bool CommitAction(object sender, PerformanceParam performanceParam)
        {
            var returnValue = true;
            if (!(performanceParam.EnumActionType is CellBlockAction cellBlockBaseAction))
                returnValue = false;
            else if (cellBlockBaseAction != CellBlockAction.GetHit)
                returnValue = false;
            else 
                Destroy();
            while (ApplyGravity())
            {}
            return returnValue;
        }
        
        private bool MoveTo(Vector3Int? coords, CellBlockViewAction viewAction = CellBlockViewAction.MoveToCoords)
        {
            if (coords == null)
                throw new ArgumentException("Performance params doesn't contains coords");
            
            if (!ParentCellGrid.TryMoveCellObjectTo(coords.Value, Id)) return false;
            
            var viewActionPerformanceParams = new ActPerformanceParam<CellBlockViewAction>(viewAction,
                vector3IntParam: coords.Value);
            _commitReaction.Invoke(this, viewActionPerformanceParams);
            return true;
        }
        
        private bool ApplyGravity()
        {
            var targetCoords = Coords + Vector3Int.down;
            return MoveTo(targetCoords, CellBlockViewAction.ApplyGravity);
        }
    }
}