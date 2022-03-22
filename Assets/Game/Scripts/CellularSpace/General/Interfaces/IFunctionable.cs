namespace Game.Scripts.CellularSpace.General.Interfaces
{
    public interface IFunctionable<TData>
    {
        TData CommitAction(TData param);
    }
}