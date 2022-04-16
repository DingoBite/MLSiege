using Game.Scripts.ModulesStartPoints;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts
{
    public class StartPoint : MonoBehaviour
    {
        [SerializeField] private GridLogicStartPoint _gridLogicStartPoint;
        [SerializeField] private InputHandlersStartPoint _inputHandlersStartPoint;
        [SerializeField] private UIStartPoint _uiStartPoint;

        private void Start()
        {
            _gridLogicStartPoint.Init();
            _uiStartPoint.Init();
            _inputHandlersStartPoint.Init(_gridLogicStartPoint);
        }
    }
}