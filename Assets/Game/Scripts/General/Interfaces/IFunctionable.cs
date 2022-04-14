namespace Game.Scripts.General.Interfaces
{
    public interface IFunctionable<TData>
    {
        TData CommitAction(TData param);
    }
}