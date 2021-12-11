using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.Blocks.Repositories;
using Assets.Siege.Model.BlockSpace.General.Interfaces;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.Blocks;
using Zenject;

namespace Assets.Siege.View.DIInstallers
{
    public class BlockSpaceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IRepository<FrameBlock>>().To<FrameBlockRepository>().AsTransient().NonLazy();

            Container.Bind<IBlockSpace<FrameBlock, BlockInfo, MonoBlock>>().To<FrameBlockSpace>().AsSingle();
        }
    }
}