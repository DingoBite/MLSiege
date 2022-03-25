using System;
using Game.Scripts.CellularSpace.General.Interfaces;
using Game.Scripts.Time.Interfaces;
using UnityEngine;

namespace Game.Scripts.View.InputControllers
{
    public class ActableMousePicker<TActParam> : IUpdatable
    {
        private Camera _camera;
        public event Action<Transform> CommitFuncEvent;
        
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
                if (!hit.transform.TryGetComponent<IActable<TActParam>>(out var interactable))
                    return;
                if (CommitFuncEvent == null) return;
                CommitFuncEvent.Invoke(hit.transform);
            }
        }
    }
}
