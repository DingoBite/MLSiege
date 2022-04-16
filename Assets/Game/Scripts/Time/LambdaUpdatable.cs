using System;
using Game.Scripts.Time.Interfaces;

namespace Game.Scripts.Time
{
    public class LambdaUpdatable : IUpdatable
    {
        private readonly Action _updateAction;

        public LambdaUpdatable(Action updateAction)
        {
            _updateAction = updateAction;
        }

        public void OnUpdate()
        {
            _updateAction?.Invoke();
        }
    }
}