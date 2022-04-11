using Game.Scripts.CellularSpace;
using UnityEngine;
using Zenject;

namespace Game.Scripts.ModulesStartPoints
{
    public class GridLogicStartPoint : MonoBehaviour
    {
        [SerializeField] private Grid _grid;
        [SerializeField] private Grid _gameGrid;

        [Inject] private IGridFacade _gridFacade;
        public IGridFacade GridFacade => _gridFacade;
        
        public void Init()
        {
            _gridFacade.Init(_grid, _gameGrid);
        }
    }
}