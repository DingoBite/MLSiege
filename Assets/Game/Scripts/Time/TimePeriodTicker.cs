using System;

namespace Game.Scripts.Time
{
    public class TimePeriodTicker : AbstractUpdateTicker
    {
        private float _currentTimeSpan;
        private float _secondsTimePeriod;

        public float SecondsTimePeriod
        {
            get => _secondsTimePeriod;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException($"value = {value} <= 0");
                _secondsTimePeriod = value;
            }
        }

        protected override void OnUpdate()
        {
            _currentTimeSpan += UnityEngine.Time.deltaTime;
            if (Math.Abs(_currentTimeSpan - _secondsTimePeriod) < float.Epsilon)
            {
                foreach (var updatable in _updatableRepository.GetValues())
                {
                    updatable?.OnUpdate();
                }
            }
            _currentTimeSpan = 0;
        }
    }
}