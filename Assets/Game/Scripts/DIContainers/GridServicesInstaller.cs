using Game.Scripts.CellularSpace.CellStorages;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.CellularSpace.GridShape;
using Game.Scripts.CellularSpace.GridShape.CoordsConverters;
using Game.Scripts.CellularSpace.GridShape.CoordsConverters.Interfaces;
using Game.Scripts.CellularSpace.GridShape.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Scripts.DIContainers
{
    public class GridServicesInstaller : MonoInstaller
    {
        [SerializeField] private GridLevel _gridLevelPrefab;
        public override void InstallBindings()
        {
            Container.Bind<IGridLevelsManager>().To<GridLevelsManager>().AsTransient().WithArguments(_gridLevelPrefab);
            Container.Bind<IGridCoordsConverter>().To<GridCoordsConverter>().AsTransient();
            Container.Bind<ICellGrid>().To<CellGrid>().AsTransient();
        }
    }
}