namespace Game.Scripts.General.Interfaces
{
    public interface IModifier<TValue>
    {
        TValue ModifyChangeValue(TValue value);
    }
}