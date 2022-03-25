using System;
using Game.Scripts.Time.Interfaces;
using UnityEngine;

namespace Game.Scripts.View.InputControllers
{
    public class KeyInputHandler : IUpdatable
    {
        private readonly KeyCode _keyCode;
        
        public event Action OnGetKeyDownEvent;

        public KeyInputHandler(KeyCode keyCode)
        {
            _keyCode = keyCode;
        }
        
        public void OnUpdate()
        {
            if (Input.GetKey(_keyCode))
            {
                OnGetKeyDownEvent?.Invoke();
            }
        }
    }
}