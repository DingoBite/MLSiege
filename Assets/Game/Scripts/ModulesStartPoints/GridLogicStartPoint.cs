using Game.Scripts.CellularSpace.GridShape;
using UnityEngine;
using Zenject;

namespace Game.Scripts.View
{
    public class GridLogicStartPoint : MonoBehaviour
    {
        [SerializeField] private Grid _grid;

        [Inject] private IGridFacade _gridFacade;
        public IGridFacade GridFacade => _gridFacade;
        
        public void Init()
        {
            _gridFacade.Init(_grid);
        }
    }
}