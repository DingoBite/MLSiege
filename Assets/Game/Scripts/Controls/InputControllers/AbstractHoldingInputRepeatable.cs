using Game.Scripts.Controls.InputControllers.Interfaces;
using Game.Scripts.Time.Interfaces;

namespace Game.Scripts.Controls.InputControllers
{
    public abstract class AbstractHoldingInputRepeatable<THoldingParam, TRepeatableAction, TRepeatParam>
        : IRepeatableInputAction<THoldingParam, TRepeatableAction, TRepeatParam>, IUpdatable
    {
        private THoldingParam _holdingParam;
        private TRepeatableAction _holdingRepeatableAction;
        private TRepeatParam _repeatParam;

        protected AbstractHoldingInputRepeatable()
        {
        }

        protected AbstractHoldingInputRepeatable(THoldingParam holdingParam, TRepeatableAction holdingRepeatableAction, TRepeatParam repeatParam)
        {
            _holdingParam = holdingParam;
            _holdingRepeatableAction = holdingRepeatableAction;
            _repeatParam = repeatParam;
        }

        public void Init(THoldingParam holdingParam, TRepeatableAction holdingRepeatableAction, TRepeatParam repeatParam)
        {
            _holdingParam = holdingParam;
            _holdingRepeatableAction = holdingRepeatableAction;
            _repeatParam = repeatParam;
        }

        public abstract void InputAction(THoldingParam holdingParam, TRepeatableAction holdingRepeatableAction, TRepeatParam repeatParam);

        public void OnUpdate() => InputAction(_holdingParam, _holdingRepeatableAction, _repeatParam);
    }
}