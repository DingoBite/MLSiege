using System;

namespace Game.Scripts.Time.Interfaces
{
    public interface IOneActUpdateTicker
    {
        void AddAction(Action action);
        void RemoveAction(Action action);
    }
}