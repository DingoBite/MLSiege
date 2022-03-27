using System;
using Game.Scripts.Time.Interfaces;
using Game.Scripts.View.CellObjects;
using UnityEngine;

namespace Game.Scripts.View.InputControllers.MousePicker
{
    public class ActableMousePicker<TActParam> : IUpdatable
    {
        private Camera _camera;
        public event Action<int> CommitFuncEvent;
        
        public void Init(Camera camera)
        {
            _camera = camera;
        }
        
        public void OnUpdate()
        {
            if (!Input.GetMouseButtonDown(0) || _camera == null) return;
            
            var raycast = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(raycast, out var hit))
            {
                if (!hit.transform.TryGetComponent<AbstractMonoCellObject>(out var monoCellObject))
                    return;

                CommitFuncEvent?.Invoke(monoCellObject.Id);
            }
            else
                CommitFuncEvent?.Invoke(-1);
        }
    }
}
