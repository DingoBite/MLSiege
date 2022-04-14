using Game.Scripts.ModulesStartPoints;
using UnityEngine;

namespace Game.Scripts
{
    public class StartPoint : MonoBehaviour
    {
        [SerializeField] private GridLogicStartPoint _gridLogicStartPoint;
        [SerializeField] private InputHandlersStartPoint _inputHandlersStartPoint;
        [SerializeField] private UIStartPoint _uiStartPoint;

        private HandlersLoader _handlersLoader;
        
        private void Start()
        {
            _gridLogicStartPoint.Init();
            _inputHandlersStartPoint.Init();
            _uiStartPoint.Init();
            _handlersLoader = new HandlersLoader(_gridLogicStartPoint);
            _handlersLoader.LoadDependencies(_inputHandlersStartPoint);
        }
    }
}