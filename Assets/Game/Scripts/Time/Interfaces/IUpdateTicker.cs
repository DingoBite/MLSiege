namespace Game.Scripts.Time.Interfaces
{
    public interface IUpdateTicker
    {
        int AddUpdatable(IUpdatable updatable);
        void RemoveUpdatable(int id);
        void RemoveUpdatable(IUpdatable updatable);
    }
}