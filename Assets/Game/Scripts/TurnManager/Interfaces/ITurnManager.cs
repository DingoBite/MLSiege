using System;
using Game.Scripts.TurnManager.Enums;

namespace Game.Scripts.TurnManager.Interfaces
{
    public interface ITurnManager
    {
        void Process();
        void AddAction(TurnState turnState, Action action);
    }
}