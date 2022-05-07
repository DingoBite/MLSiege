using System;
using Game.Scripts.Time.Interfaces;
using UnityEngine;

namespace Game.Scripts.Time
{
    public class OneActUpdateTicker : MonoBehaviour, IOneActUpdateTicker
    {
        private event Action OneActActions;
        
        public void AddAction(Action action) => OneActActions += action;

        public void RemoveAction(Action action) => OneActActions -= action;

        private void Update()
        {
            if (OneActActions == null) return;
            
            OneActActions.Invoke();
            OneActActions = null;
        }
    }
}