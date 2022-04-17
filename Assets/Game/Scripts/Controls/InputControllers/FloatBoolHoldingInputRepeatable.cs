using System;

namespace Game.Scripts.Controls.InputControllers
{
    public class FloatBoolHoldingInputRepeatable : AbstractHoldingInputRepeatable<Func<bool>, Action, Func<bool>>
    {
        public FloatBoolHoldingInputRepeatable()
        {
        }

        public FloatBoolHoldingInputRepeatable(Func<bool> holdingParam, Action holdingRepeatableAction, Func<bool> repeatParam) 
            : base(holdingParam, holdingRepeatableAction, repeatParam)
        {
        }

        public override void InputAction(Func<bool> holdingCondition, Action holdingRepeatableAction, Func<bool> repeatFlag)
        {
            if (holdingCondition() && repeatFlag()) holdingRepeatableAction?.Invoke();
        }
    }
}