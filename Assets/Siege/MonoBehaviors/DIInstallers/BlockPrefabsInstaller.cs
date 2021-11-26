using System.Collections.Generic;
using System.Linq;
using Assets.Siege.Model.General.Enums;
using Assets.Siege.MonoBehaviors.CellableObjects;
using UnityEngine;
using Zenject;

namespace Assets.Siege.MonoBehaviors.DIInstallers
{
    public class BlockPrefabsInstaller : MonoInstaller
    {
        [SerializeField] private List<MonoBlock> _prefabs;

        public override void InstallBindings()
        {
            var blockPrefabs = _prefabs.ToDictionary(mb => mb.BlockType(), mb => mb);

            Container.Bind<IDictionary<BlockType, MonoBlock>>().FromInstance(blockPrefabs).AsTransient();
        }
    }
}