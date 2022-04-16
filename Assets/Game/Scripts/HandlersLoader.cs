using Game.Scripts.General.FlexibleDataApi;
using Game.Scripts.ModulesStartPoints;

namespace Game.Scripts
{
    public class HandlersLoader
    {
        private GridLogicStartPoint _gridLogicStartPoint;

        public HandlersLoader(GridLogicStartPoint gridLogicStartPoint)
        {
            _gridLogicStartPoint = gridLogicStartPoint;
        }

        public void LoadDependencies(InputHandlersStartPoint inputHandlersStartPoint)
        {
            inputHandlersStartPoint.CellObjectMousePickEvent += OnMousePick;
            inputHandlersStartPoint.DeleteDownEvent += OnDeleteDown;
            inputHandlersStartPoint.GKeyDownEvent += OnGDown;
            inputHandlersStartPoint._wasdssHandler.WInputHandler.OnGetKeyDownEvent += OnWDown;
            inputHandlersStartPoint._wasdssHandler.AInputHandler.OnGetKeyDownEvent += OnADown;
            inputHandlersStartPoint._wasdssHandler.SInputHandler.OnGetKeyDownEvent += OnSDown;
            inputHandlersStartPoint._wasdssHandler.DInputHandler.OnGetKeyDownEvent += OnDDown;
            inputHandlersStartPoint._wasdssHandler.ShiftInputHandler.OnGetKeyDownEvent += OnShiftDown;
            inputHandlersStartPoint._wasdssHandler.SpaceInputHandler.OnGetKeyDownEvent += OnSpaceDown;
        }
        
        private void OnMousePick(int id) => _gridLogicStartPoint.GridFacade.CommitSelectAction(id);
        
        private void OnDeleteDown() => 
            _gridLogicStartPoint.GridFacade.CommitActionToSelected(CellObjectBaseActions.Dispose);

        private void OnGDown() =>
            _gridLogicStartPoint.GridFacade.ApplyGlobalAction();
        
        private void OnWDown() => 
            _gridLogicStartPoint.GridFacade.CommitActionToSelected(CellObjectBaseActions.MoveForward);
        
        private void OnADown() => 
            _gridLogicStartPoint.GridFacade.CommitActionToSelected(CellObjectBaseActions.MoveLeft);
        
        private void OnSDown() =>
            _gridLogicStartPoint.GridFacade.CommitActionToSelected(CellObjectBaseActions.MoveBack);
        
        private void OnDDown() => 
            _gridLogicStartPoint.GridFacade.CommitActionToSelected(CellObjectBaseActions.MoveRight);
        
        private void OnSpaceDown() => 
            _gridLogicStartPoint.GridFacade.CommitActionToSelected(CellObjectBaseActions.MoveUp);

        private void OnShiftDown() =>
            _gridLogicStartPoint.GridFacade.CommitActionToSelected(CellObjectBaseActions.MoveDown);
    }
}