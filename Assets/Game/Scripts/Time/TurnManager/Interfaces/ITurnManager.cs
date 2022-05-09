using System;
using Game.Scripts.Time.TurnManager.Enums;

namespace Game.Scripts.Time.TurnManager.Interfaces
{
    public interface ITurnManager
    {
        void Process();
        void AddAction(TurnState turnState, Action action);
    }
}