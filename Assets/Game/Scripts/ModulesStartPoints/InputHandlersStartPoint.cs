using System;
using Game.Scripts.Time.Interfaces;
using Game.Scripts.View.InputControllers;
using UnityEngine;
using Zenject;

namespace Game.Scripts.ModulesStartPoints
{
    public class InputHandlersStartPoint : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        [Inject] private IUpdateTicker _updateTicker;
        
        private readonly CellObjectMousePicker _cellObjectMousePicker = new CellObjectMousePicker();
        private int _mousePickerId;
        
        private readonly KeyInputHandler _spaceInputHandler = new KeyInputHandler(KeyCode.Space);
        private int _spaceHandlerId;
        
        public event Action<Transform> CellObjectMousePickEvent
        {
            add => _cellObjectMousePicker.CommitFuncEvent += value;
            remove => _cellObjectMousePicker.CommitFuncEvent -= value;
        }
        
        public event Action SpaceDownEvent
        {
            add => _spaceInputHandler.OnGetKeyDownEvent += value;
            remove => _spaceInputHandler.OnGetKeyDownEvent -= value;
        }
        
        public void Init()
        {
            _cellObjectMousePicker.Init(_camera);
            _mousePickerId = _updateTicker.AddUpdatable(_cellObjectMousePicker);
            _spaceHandlerId = _updateTicker.AddUpdatable(_spaceInputHandler);
        }

        private void Subscribe()
        {
            if (_cellObjectMousePicker != null && !_updateTicker.Contains(_mousePickerId)) 
                _updateTicker.AddUpdatable(_cellObjectMousePicker);
            
            if (_spaceInputHandler != null && !_updateTicker.Contains(_spaceInputHandler)) 
                _updateTicker.AddUpdatable(_spaceInputHandler);
        }

        private void Unsubscribe()
        {
            _updateTicker.RemoveUpdatable(_cellObjectMousePicker);
            _updateTicker.RemoveUpdatable(_spaceInputHandler);
        }

        private void OnEnable()
        {
            if (_updateTicker == null) return;
            Subscribe();
        }

        private void OnDisable()
        {
            if (_updateTicker == null) return;
            Unsubscribe();
        }
    }
}