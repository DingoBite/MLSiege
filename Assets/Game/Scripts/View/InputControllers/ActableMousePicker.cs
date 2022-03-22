using System;
using Game.Scripts.Time.Interfaces;
using Game.Scripts.View.CellObjects.Interfaces;
using UnityEngine;

namespace Game.Scripts.View.InputControllers
{
    public class ActableMousePicker<TActParam> : IUpdatable
    {
        private Camera _camera;
        public event Func<Transform, TActParam> CommitFuncEvent;
        
        public void Init(Camera camera)
        {
            _camera = camera;
        }
        
        public void OnUpdate()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            
            var raycast = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(raycast, out var hit))
            {
                if (!hit.transform.TryGetComponent<IActable<TActParam>>(out var interactable))
                    return;
                if (CommitFuncEvent == null) return;
                var commitResult = CommitFuncEvent.Invoke(hit.transform);
                interactable.CommitAction(commitResult);
            }
        }
    }
}
