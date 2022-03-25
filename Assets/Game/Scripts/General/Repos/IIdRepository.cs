using System.Collections.Generic;

namespace Game.Scripts.General.Repos
{
    public interface IIdRepository<TValue>
    {
        bool Contains(int id);
        bool Contains(TValue updatable);
        int Add(TValue value);
        void Remove(int id);
        void Remove(TValue value);
        IEnumerable<TValue> GetValues();
    }
}