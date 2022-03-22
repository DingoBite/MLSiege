using System;
using Game.Scripts.CellularSpace.CellStorages.CellObjects;
using Game.Scripts.Time.Interfaces;
using Game.Scripts.View.InputControllers;
using UnityEngine;
using Zenject;

namespace Game.Scripts
{
    public class InputHandlersStartPoint : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        [Inject] private IUpdateTicker _updateTicker;
        
        private readonly CellObjectMousePicker _cellObjectMousePicker = new CellObjectMousePicker();
    
        public event Func<Transform, FlexibleData> CellObjectMousePickEvent
        {
            add => _cellObjectMousePicker.CommitFuncEvent += value;
            remove => _cellObjectMousePicker.CommitFuncEvent -= value;
        }
        
        public void Init()
        {
            _cellObjectMousePicker.Init(_camera);
            _updateTicker.AddUpdatable(_cellObjectMousePicker);
        }
    }
}