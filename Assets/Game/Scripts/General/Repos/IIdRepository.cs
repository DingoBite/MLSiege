using System.Collections.Generic;

namespace Game.Scripts.General.Repos
{
    public interface IIdRepository<TValue>
    {
        int PeekId();
        bool Contains(int id);
        bool Contains(TValue updatable);
        TValue Get(int id);
        int Add(TValue value);
        void Remove(int id);
        void Remove(TValue value);
        IEnumerable<TValue> GetValues();
    }
}