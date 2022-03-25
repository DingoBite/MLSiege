namespace Game.Scripts.Time.Interfaces
{
    public interface IUpdateTicker
    {
        bool Contains(int id);
        bool Contains(IUpdatable updatable);
        int AddUpdatable(IUpdatable updatable);
        void RemoveUpdatable(int id);
        void RemoveUpdatable(IUpdatable updatable);
    }
}