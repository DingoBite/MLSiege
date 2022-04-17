namespace Game.Scripts.Controls.InputControllers.Interfaces
{
    public interface IRepeatableInputAction<in THoldingParam, in TRepeatableAction, in TRepeatParam>
    {
        void InputAction(THoldingParam holdingParam, TRepeatableAction holdingRepeatableAction, TRepeatParam repeatParam);
    }
}