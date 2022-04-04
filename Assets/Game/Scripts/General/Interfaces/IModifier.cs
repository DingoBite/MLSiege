namespace Game.Scripts.CellularSpace.General.Interfaces
{
    public interface IModifier<TValue>
    {
        TValue ModifyChangeValue(TValue value);
    }
}