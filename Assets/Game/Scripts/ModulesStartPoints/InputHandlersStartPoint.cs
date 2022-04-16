using System;
using Game.Scripts.Time;
using Game.Scripts.View.InputControllers;
using Game.Scripts.View.InputControllers.KeyHandler;
using Game.Scripts.View.InputControllers.MousePicker;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Scripts.ModulesStartPoints
{
    public class InputHandlersStartPoint : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        [Inject] private UpdateTicker _updateTicker;
        
        private readonly CellObjectMousePicker _mousePicker = new CellObjectMousePicker();
        private int _mousePickerId;
        
        private readonly KeyInputHandler _deleteInputHandler = new KeyInputHandler(KeyCode.Delete);
        private int _deleteHandlerId;
        
        private readonly KeyInputHandler _gKeyInputHandler = new KeyInputHandler(KeyCode.G);
        private int _gKeyInputHandlerId;
        
        public readonly  WASDSSHandler _wasdssHandler = new WASDSSHandler();
        private int _wasdssHandlerId;
        
        private GameControls _gameControls;
        private int _gravityUpdatableId;

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
        
        public void Init(GridLogicStartPoint gridLogicStartPoint)
        {
            _gameControls = new GameControls();
            var kek = new RepeatableInputHandler();
            kek.Init(() => _gameControls.Agent.Gravity, () => Debug.Log(1), 0.3);
            _gravityUpdatableId = _updateTicker.AddUpdatable(kek);
            _gameControls.Agent.Enable();
            // _mousePicker.Init(_camera);
            // _mousePickerId = _updateTicker.AddUpdatable(_mousePicker);
            // _deleteHandlerId = _updateTicker.AddUpdatable(_deleteInputHandler);
            // _gKeyInputHandlerId = _updateTicker.AddUpdatable(_gKeyInputHandler);
            // _wasdssHandlerId = _updateTicker.AddUpdatable(_wasdssHandler);
        }

        private void Subscribe()
        {
            if (!_updateTicker.Contains(_mousePickerId)) 
                _updateTicker.AddUpdatable(_mousePicker);
            
            if (!_updateTicker.Contains(_deleteHandlerId)) 
                _updateTicker.AddUpdatable(_deleteInputHandler);
            
            if (!_updateTicker.Contains(_gKeyInputHandlerId)) 
                _updateTicker.AddUpdatable(_gKeyInputHandler);
            
            if (!_updateTicker.Contains(_wasdssHandlerId)) 
                _updateTicker.AddUpdatable(_wasdssHandler);
        }

        private void Unsubscribe()
        {
            _updateTicker.RemoveUpdatable(_mousePickerId);
            _updateTicker.RemoveUpdatable(_deleteHandlerId);
            _updateTicker.RemoveUpdatable(_gKeyInputHandlerId);
            _updateTicker.RemoveUpdatable(_wasdssHandlerId);
        }
        private void OnEnable()
        {
            _gameControls?.Agent.Enable();
            if (_updateTicker == null) return;
            //Subscribe();
        }

        private void OnDisable()
        {
            _gameControls?.Agent.Disable();
            if (_updateTicker == null) return;
            //Unsubscribe();
        }
    }
}