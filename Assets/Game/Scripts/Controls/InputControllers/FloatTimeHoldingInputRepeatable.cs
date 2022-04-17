using System;

namespace Game.Scripts.Controls.InputControllers
{
    public class FloatTimeHoldingInputRepeatable : AbstractHoldingInputRepeatable<Func<bool>, Action, double>
    {
        private float _currentTime;

        public FloatTimeHoldingInputRepeatable()
        {
        }

        public FloatTimeHoldingInputRepeatable(Func<bool> holdingParam, Action holdingRepeatableAction, double repeatParam) 
            : base(holdingParam, holdingRepeatableAction, repeatParam)
        {
        }

        public override void InputAction(Func<bool> holdingCondition, Action holdingRepeatableAction, double repeatParam)
        {
            if (!holdingCondition())
            {
                _currentTime = 0;
                return;
            }
            
            if (_currentTime >= repeatParam)
            {
                _currentTime = 0;
                holdingRepeatableAction?.Invoke();
                return;
            }
            _currentTime += UnityEngine.Time.deltaTime;
        }
    }
}