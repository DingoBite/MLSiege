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
            _inputHandlersStartPoint.CellObjectMousePickEvent += OnMousePick;
            _inputHandlersStartPoint.DeleteDownEvent += OnDeleteDown;
            _inputHandlersStartPoint.GKeyDownEvent += OnGDown;
        }

        private void OnMousePick(int id) => _gridLogicStartPoint.GridFacade.CommitSelectAction(id);
        
        private void OnDeleteDown() => 
            _gridLogicStartPoint.GridFacade.CommitAction(new ActionPerformanceParams<CellObjectBaseAction>(CellObjectBaseAction.Dispose));
        
        private void OnGDown() => 
            _gridLogicStartPoint.GridFacade.CommitAction(new ActionPerformanceParams<CellObjectBaseAction>(CellObjectBaseAction.ApplyGravity));
    }
}