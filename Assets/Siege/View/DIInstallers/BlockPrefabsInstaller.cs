using System.Collections.Generic;
using System.Linq;
using Assets.Siege.Model.General.Enums;
using Assets.Siege.View.Blocks;
using UnityEngine;
using Zenject;

namespace Assets.Siege.View.DIInstallers
{
    public class BlockPrefabsInstaller : MonoInstaller
    {
        [SerializeField] private List<MonoBlock> _prefabs;

        public override void InstallBindings()
        {
            var blockPrefabs = _prefabs
                .ToDictionary(mb => mb.GetInfo().BlockType, mb => mb);

            Container.Bind<IDictionary<BlockType, MonoBlock>>().FromInstance(blockPrefabs).AsSingle();
        }
    }
}