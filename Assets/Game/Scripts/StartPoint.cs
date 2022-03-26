using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.General.FlexibleDataApi;
using Game.Scripts.ModulesStartPoints;
using UnityEngine;

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
            _inputHandlersStartPoint.Init();
            _uiStartPoint.Init();

            LoadDependencies();
        }

        private void LoadDependencies()
        {
            _inputHandlersStartPoint.CellObjectMousePickEvent += 
                tr => _gridLogicStartPoint.GridFacade.CommitSelectAction(tr.position);
            _inputHandlersStartPoint.SpaceDownEvent += () => 
                _gridLogicStartPoint.GridFacade.CommitAction(new ActionPerformanceParams<CellObjectBaseAction>(CellObjectBaseAction.Dispose));
        }
    }
}