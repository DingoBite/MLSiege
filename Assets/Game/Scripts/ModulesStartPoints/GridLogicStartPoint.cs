using Game.Scripts.CellularSpace;
using Game.Scripts.TurnManager.Interfaces;
using UnityEngine;

namespace Game.Scripts.ModulesStartPoints
{
    public class GridLogicStartPoint : MonoBehaviour
    {
        [SerializeField] private Grid _grid;
        [SerializeField] private Grid _gameGrid;

        private IGridFacade _gridFacade;
        private ITurnManager _turnManager;
        
        public void Init(IGridFacade gridFacade, ITurnManager turnManager)
        {
            foreach (Transform child in _gameGrid.transform)
            {
                Destroy(child.gameObject);
            }
            _grid.gameObject.SetActive(false);
            _gridFacade = gridFacade;
            _turnManager = turnManager;
            _gridFacade.Init(_grid, _gameGrid);
        }

        public void ReInit()
        {
            foreach (Transform child in _gameGrid.transform)
            {
                Destroy(child.gameObject);
            }
            _grid.gameObject.SetActive(false);
            _gridFacade.ReInit(_gameGrid);
        }
    }
}