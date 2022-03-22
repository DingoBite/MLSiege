using UnityEngine;

namespace Game.Scripts.View
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
                tr => _gridLogicStartPoint.GridFacade.CommitAction(tr.position);
        }
    }
}