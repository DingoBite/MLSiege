using Game.Scripts.General.FlexibleDataApi;

namespace Game.Scripts.CellularSpace.General.Interfaces
{
    public interface IActable<in TParam>
    {
        void CommitAction(object sender, PerformanceParams performanceParams);
    }
}