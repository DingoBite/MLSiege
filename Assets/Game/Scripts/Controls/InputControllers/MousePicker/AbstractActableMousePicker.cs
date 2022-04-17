using Game.Scripts.General.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Controls.InputControllers.MousePicker
{
    public abstract class AbstractActableMousePicker<TActParam> where TActParam : IIdentifiable
    {
        public void Pick(Camera camera)
        {
            var mousePosition = Mouse.current.position.ReadValue();
            var raycast = camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(raycast, out var hit))
            {
                if (!hit.transform.TryGetComponent<TActParam>(out var monoCellObject))
                    return;

                OnMousePick(monoCellObject.Id);
            }
            else
                OnMousePick(-1);
        }

        protected abstract void OnMousePick(int id);
    }
}
