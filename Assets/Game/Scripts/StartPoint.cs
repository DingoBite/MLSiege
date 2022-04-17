using Game.Scripts.Controls;
using Game.Scripts.ModulesStartPoints;
using UnityEngine;

namespace Game.Scripts
{
    public class StartPoint : MonoBehaviour
    {
        [SerializeField] private GridLogicStartPoint _gridLogicStartPoint;
        [SerializeField] private InputHandlersStartPoint _inputHandlersStartPoint;
        [SerializeField] private UIStartPoint _uiStartPoint;
        private GameControls _gameControls;

        private void Start()
        {
            _gridLogicStartPoint.Init();
            _uiStartPoint.Init();
            _inputHandlersStartPoint.Init(_gridLogicStartPoint);
        }
    }
}