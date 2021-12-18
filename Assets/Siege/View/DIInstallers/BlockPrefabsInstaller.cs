using System.Collections.Generic;
using System.Linq;
using Assets.Siege.Model.BlockSpace.Blocks.Enums;
using Assets.Siege.Model.BlockSpace.General;
using Assets.Siege.Model.BlockSpace.General.Interfaces;
using Assets.Siege.View.Blocks;
using UnityEngine;
using Zenject;

namespace Assets.Siege.View.DIInstallers
{
    public class BlockPrefabsInstaller : MonoInstaller
    {
        [SerializeField] private List<MonoBlock> _blockPrefabs;

        public override void InstallBindings()
        {
            var blockPrefabs = _blockPrefabs
                .ToDictionary(mb => mb.GetInfo().BlockType, mb => mb);

            Container.Bind<IPrefabsByType<BlockType, MonoBlock>>().FromInstance(new PrefabsByType<BlockType, MonoBlock>(blockPrefabs))
                .AsSingle().NonLazy();
        }
    }
}