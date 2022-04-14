namespace Game.Scripts.General.Interfaces
{
    public interface IActable<in TParam>
    {
        void CommitAction(object sender, TParam performanceParam);
    }
    
    public interface IActableReturnsBool<in TParam>
    {
        bool CommitAction(object sender, TParam performanceParam);
    }
}