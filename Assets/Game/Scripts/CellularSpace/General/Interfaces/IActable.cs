namespace Game.Scripts.CellularSpace.General.Interfaces
{
    public interface IActable<in TParam>
    {
        void CommitAction(TParam param);
    }
}