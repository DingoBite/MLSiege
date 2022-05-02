using System;
using Game.Scripts.CellularSpace;
using Game.Scripts.Controls.InputControllers;
using Game.Scripts.Controls.InputControllers.MousePicker;
using Game.Scripts.General.Enums;
using Game.Scripts.General.FlexibleDataApi;
using Game.Scripts.Time;
using UnityEngine;
using Zenject;

namespace Game.Scripts.ModulesStartPoints
{
    public class InputHandlersStartPoint : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        [Inject] private UpdateTicker _updateTicker;

        private bool _isInit;
        private GameControls _gameControls;
        
        private FloatTimeHoldingInputRepeatable _globalActionRepeatable;
        private int _globalActionRepeatableId = -1;

        private VectorTimeHoldingInputRepeatable _movementInputRepeatable;
        private int _movementInputRepeatableId = -1;
        
        public void Init(IGridFacade gridFacade)
        {
            if (_isInit)
                throw new Exception($"Try to reinit {typeof(InputHandlersStartPoint)}");
            
            if (_camera == null) 
                _camera = Camera.main;
            _gameControls = new GameControls();

            _globalActionRepeatable = new FloatTimeHoldingInputRepeatable(
                () => _gameControls.World.GlobalAction.ReadValue<float>() >= float.Epsilon,
                gridFacade.ApplyGlobalAction,
                0.2
                );
            
            _movementInputRepeatable = new VectorTimeHoldingInputRepeatable(
                () => _gameControls.CellObject.Movement.ReadValue<Vector2>(),
                direction => MoveBlockAction(gridFacade, direction),
                0.2
                );

            var leftMousePicker = new CellObjectMousePicker(gridFacade.CommitSelectAction);
            _gameControls.ObjectPicker.SelectObject.started += c => leftMousePicker.Pick(_camera);
            var rightMousePicker = new CellObjectMousePicker(gridFacade.CommitSelectedPathFind);
            _gameControls.ObjectPicker.PathFind.started += c => rightMousePicker.Pick(_camera);
            
            _isInit = true;
            Subscribe();
        }

        private void MoveBlockAction(IGridFacade gridFacade, Direction direction)
        {
            if (direction == Direction.Stop) return;
            gridFacade.CommitActionToSelected(PerformanceParamFromDirection(direction));
        }

        private PerformanceParam PerformanceParamFromDirection(Direction direction)
        {
            return direction switch
            {
                Direction.Up => CellObjectBaseActions.MoveForward,
                Direction.Left => CellObjectBaseActions.MoveLeft,
                Direction.Down => CellObjectBaseActions.MoveBack,
                Direction.Right => CellObjectBaseActions.MoveRight,
                Direction.Stop => null,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
        
        private void Subscribe()
        {
            if (!_updateTicker.Contains(_globalActionRepeatableId)) 
                _globalActionRepeatableId = _updateTicker.AddUpdatable(_globalActionRepeatable);
            
            if (!_updateTicker.Contains(_movementInputRepeatableId)) 
                _movementInputRepeatableId = _updateTicker.AddUpdatable(_movementInputRepeatable);

            _gameControls?.Enable();
        }

        private void Unsubscribe()
        {
            _updateTicker.RemoveUpdatable(_globalActionRepeatableId);
            _updateTicker.RemoveUpdatable(_movementInputRepeatableId);
            
            _gameControls?.Disable();
        }

        private void OnEnable()
        {
            if (!_isInit) return;
            if (_updateTicker == null) throw new NullReferenceException();
            Subscribe();
        }

        private void OnDisable()
        {
            if (!_isInit) return;
            if (_updateTicker == null) return;
            Unsubscribe();
        }
    }
}