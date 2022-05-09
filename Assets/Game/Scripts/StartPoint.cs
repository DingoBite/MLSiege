using System;
using Game.Scripts.CellularSpace;
using Game.Scripts.ModulesStartPoints;
using Game.Scripts.SiegeML;
using Game.Scripts.SiegeML.ActionResolvers;
using Game.Scripts.SiegeML.Learning;
using Game.Scripts.SiegeML.ObservationsCollectors;
using Game.Scripts.Time.Interfaces;
using Game.Scripts.Time.TurnManager.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Scripts
{
    public class StartPoint : MonoBehaviour
    {
        [SerializeField] private GridLogicStartPoint _gridLogicStartPoint;
        [SerializeField] private InputHandlersStartPoint _inputHandlersStartPoint;
        [SerializeField] private UIStartPoint _uiStartPoint;

        [SerializeField] private SiegeAgents _siegeAgents;

        [SerializeField] private int _agentsNearGoalToWin;
        
        [Inject] private IGridFacade _gridFacade;
        
        [Inject] private IUpdateTicker _updateTicker;
        [Inject] private ITurnManager _turnManager;
        
        [Inject] private IAgentManager _agentManager;
        [Inject] private IObservationsCollector _observationsCollector;
        [Inject] private IActionResolver _actionResolver;

        private void Start()
        {
            _gridLogicStartPoint.Init(_gridFacade, _turnManager);
            _uiStartPoint.Init();
            _inputHandlersStartPoint.Init(_gridFacade, _updateTicker);
            _agentManager.Init(_gridFacade, _observationsCollector, _actionResolver, _agentsNearGoalToWin, Win, Lose);
            _siegeAgents.Init(_agentManager);
        }
        
        private void Restart()
        {
            Resources.UnloadUnusedAssets();
            _gridLogicStartPoint.ReInit();
            _agentManager.ReInit();
        }
        
        private void Win()
        {
            Debug.Log("Win");
            Restart();
        }

        private void Lose()
        {
            Debug.Log("Lose");
            Restart();
        }
    }
}