using System;
using Game.Scripts.ModulesStartPoints;
using Game.Scripts.Time.Interfaces;
using UnityEngine.InputSystem;

namespace Game.Scripts.View.InputControllers
{
    public class RepeatableInputHandler : IRepeatableInputAction, IUpdatable
    {
        private Func<InputAction> _inputAction;
        private Action _holdingRepeatableAction;
        private double _repeatSecondsStep;
        private float _currentTime;
        
        public RepeatableInputHandler()
        {
        }
        
        public RepeatableInputHandler(Func<InputAction> inputAction, Action holdingRepeatableAction, double repeatSecondsStep)
        {
            _inputAction = inputAction;
            _holdingRepeatableAction = holdingRepeatableAction;
            _repeatSecondsStep = repeatSecondsStep;
        }
        
        public void Init(Func<InputAction> inputAction, Action holdingRepeatableAction, double repeatSecondsStep)
        {
            _inputAction = inputAction;
            _holdingRepeatableAction = holdingRepeatableAction;
            _repeatSecondsStep = repeatSecondsStep;
        }
        
        public void InputAction(Func<InputAction> inputAction, Action holdingRepeatableAction, double repeatSecondsStep)
        {
            if (inputAction().ReadValue<float>() < float.Epsilon)
            {
                _currentTime = 0;
                return;
            }
            
            if (_currentTime >= repeatSecondsStep)
            {
                _currentTime = 0;
                holdingRepeatableAction?.Invoke();
                return;
            }
            _currentTime += UnityEngine.Time.deltaTime;
        }

        public void OnUpdate() => InputAction(_inputAction, _holdingRepeatableAction, _repeatSecondsStep);
    }
}