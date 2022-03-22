namespace Game.Scripts.View.CellObjects.Interfaces
{
    public interface IActable<in TParam>
    {
        void CommitAction(TParam param);
    }
}