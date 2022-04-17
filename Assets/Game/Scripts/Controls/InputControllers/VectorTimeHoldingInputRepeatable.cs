using System;
using Game.Scripts.General.Enums;
using UnityEngine;

namespace Game.Scripts.Controls.InputControllers
{
    public class VectorTimeHoldingInputRepeatable : AbstractHoldingInputRepeatable<Func<Vector2>, Action<Direction>, double>
    {
        private float _currentTime;
        private bool _state;
        //private Queue<Direction> _directionQueue = new Queue<Direction>();
        private Direction _previousDirection;
        
        public VectorTimeHoldingInputRepeatable()
        {
        }

        public VectorTimeHoldingInputRepeatable(Func<Vector2> holdingParam,  Action<Direction> holdingRepeatableAction, double repeatParam)
            : base(holdingParam, holdingRepeatableAction, repeatParam)
        {
        }

        public override void InputAction(Func<Vector2> holdingParam,  Action<Direction> holdingRepeatableAction, double repeatParam)
        {
            var direction = DirectionFromVector(holdingParam());
            if (direction == Direction.Stop)
            {
                holdingRepeatableAction?.Invoke(direction);
                _currentTime = 0;
                _state = false;
                return;
            }
            //_directionQueue.Enqueue(direction);
            if (!_state)
            {
                _previousDirection = direction;
                holdingRepeatableAction?.Invoke(direction);
                _state = true;
            }
            else if (_currentTime >= repeatParam)
            {
                _currentTime = 0;
                _previousDirection = direction;
                holdingRepeatableAction?.Invoke(direction);
                return;
            }
            _currentTime += UnityEngine.Time.deltaTime;
        }

        private Direction DirectionFromVector(Vector2 moveVector)
        {
            if (moveVector == Vector2.up) return Direction.Up;
            if (moveVector == Vector2.left) return Direction.Left;
            if (moveVector == Vector2.down) return Direction.Down;
            if (moveVector == Vector2.right) return Direction.Right;
            if (moveVector == Vector2.zero) return Direction.Stop;

            if (moveVector.y > 0)
            {
                if (moveVector.x > 0) return _previousDirection == Direction.Up ? Direction.Right : Direction.Up;
                if (moveVector.x < 0) return _previousDirection == Direction.Up ? Direction.Left : Direction.Up;
            }
            if (moveVector.y < 0)
            {
                if (moveVector.x > 0) return _previousDirection == Direction.Down ? Direction.Right : Direction.Down;
                if (moveVector.x < 0) return _previousDirection == Direction.Down ? Direction.Left : Direction.Down;
            }

            return Direction.Stop;
        }
    }
}