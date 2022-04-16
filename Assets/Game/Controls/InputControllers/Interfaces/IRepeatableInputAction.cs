using System;
using UnityEngine.InputSystem;

namespace Game.Scripts.ModulesStartPoints
{
    public interface IRepeatableInputAction
    {
        void InputAction(Func<InputAction> inputActionGetter, Action holdingRepeatableAction, double repeatSecondsStep);
    }
}