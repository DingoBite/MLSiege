using System;
using Game.Scripts.TurnManager.Enums;
using Game.Scripts.TurnManager.Interfaces;

namespace Game.Scripts.TurnManager
{
    public class WrappedTurnManager : ITurnManager
    {
        private TurnState _turnState = TurnState.TurnPreStart;

        private event Action PreStartActions;
        private event Action StartActions;
        private event Action AfterStartActions;
        private event Action PreProcessActions;
        private event Action ProcessActions;
        private event Action AfterProcessActions;
        private event Action PreEndActions;
        private event Action EndActions;
        private event Action AfterEndActions;
        
        public void Process()
        {
            switch (_turnState)
            {
                case TurnState.TurnPreStart:
                    PreStartActions?.Invoke();
                    PreStartActions = null;
                    _turnState = TurnState.TurnStart;
                    break;
                case TurnState.TurnStart:
                    StartActions?.Invoke();
                    StartActions = null;
                    _turnState = TurnState.TurnAfterStart;
                    break;
                case TurnState.TurnAfterStart:
                    AfterStartActions?.Invoke();
                    AfterStartActions = null;
                    _turnState = TurnState.TurnPreProcess;
                    break;
                case TurnState.TurnPreProcess:
                    PreProcessActions?.Invoke();
                    PreProcessActions = null;
                    _turnState = TurnState.TurnProcess;
                    break;
                case TurnState.TurnProcess:
                    ProcessActions?.Invoke();
                    ProcessActions = null;
                    _turnState = TurnState.TurnAfterProcess;
                    break;
                case TurnState.TurnAfterProcess:
                    AfterProcessActions?.Invoke();
                    AfterProcessActions = null;
                    _turnState = TurnState.TurnPreEnd;
                    break;
                case TurnState.TurnPreEnd:
                    PreEndActions?.Invoke();
                    PreEndActions = null;
                    _turnState = TurnState.TurnEnd;
                    break;
                case TurnState.TurnEnd:
                    EndActions?.Invoke();
                    EndActions = null;
                    _turnState = TurnState.TurnAfterEnd;
                    break;
                case TurnState.TurnAfterEnd:
                    AfterEndActions?.Invoke();
                    AfterEndActions = null;
                    _turnState = TurnState.TurnPreStart;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void AddAction(TurnState turnState, Action action)
        {
            switch (turnState)
            {
                case TurnState.TurnPreStart:
                    PreStartActions += action;
                    break;
                case TurnState.TurnStart:
                    StartActions += action;
                    break;
                case TurnState.TurnAfterStart:
                    AfterStartActions += action;
                    break;
                case TurnState.TurnPreProcess:
                    PreProcessActions += action;
                    break;
                case TurnState.TurnProcess:
                    ProcessActions += action;
                    break;
                case TurnState.TurnAfterProcess:
                    AfterProcessActions += action;
                    break;
                case TurnState.TurnPreEnd:
                    PreEndActions += action;
                    break;
                case TurnState.TurnEnd:
                    EndActions += action;
                    break;
                case TurnState.TurnAfterEnd:
                    AfterEndActions += action;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(turnState), turnState, null);
            }
        }
    }
}