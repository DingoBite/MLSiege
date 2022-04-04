using System;
using Game.Scripts.Time.Interfaces;
using Game.Scripts.View.InputControllers.KeyHandler;
using Game.Scripts.View.InputControllers.MousePicker;
using UnityEngine;
using Zenject;

namespace Game.Scripts.ModulesStartPoints
{
    public class InputHandlersStartPoint : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        [Inject] private IUpdateTicker _updateTicker;
        
        private readonly CellObjectMousePicker _mousePicker = new CellObjectMousePicker();
        private int _mousePickerId;
        
        private readonly KeyInputHandler _deleteInputHandler = new KeyInputHandler(KeyCode.Delete);
        private int _deleteHandlerId;
        
        private readonly KeyInputHandler _gKeyInputHandler = new KeyInputHandler(KeyCode.G);
        private int _gKeyInputHandlerId;
        
        public event Action<int> CellObjectMousePickEvent
        {
            add => _mousePicker.CommitFuncEvent += value;
            remove => _mousePicker.CommitFuncEvent -= value;
        }
        
        public event Action DeleteDownEvent
        {
            add => _deleteInputHandler.OnGetKeyDownEvent += value;
            remove => _deleteInputHandler.OnGetKeyDownEvent -= value;
        }
        
        public event Action GKeyDownEvent
        {
            add => _gKeyInputHandler.OnGetKeyDownEvent += value;
            remove => _gKeyInputHandler.OnGetKeyDownEvent -= value;
        }
        
        public void Init()
        {
            _mousePicker.Init(_camera);
            _mousePickerId = _updateTicker.AddUpdatable(_mousePicker);
            _deleteHandlerId = _updateTicker.AddUpdatable(_deleteInputHandler);
            _gKeyInputHandlerId = _updateTicker.AddUpdatable(_gKeyInputHandler);
        }

        private void Subscribe()
        {
            if (_mousePicker != null && !_updateTicker.Contains(_mousePickerId)) 
                _updateTicker.AddUpdatable(_mousePicker);
            
            if (_deleteInputHandler != null && !_updateTicker.Contains(_deleteHandlerId)) 
                _updateTicker.AddUpdatable(_deleteInputHandler);
            
            if (_gKeyInputHandler != null && !_updateTicker.Contains(_gKeyInputHandlerId)) 
                _updateTicker.AddUpdatable(_gKeyInputHandler);
        }

        private void Unsubscribe()
        {
            _updateTicker.RemoveUpdatable(_mousePickerId);
            _updateTicker.RemoveUpdatable(_deleteHandlerId);
            _updateTicker.RemoveUpdatable(_gKeyInputHandlerId);
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