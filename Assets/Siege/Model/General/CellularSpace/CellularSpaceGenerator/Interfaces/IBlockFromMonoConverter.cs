using Assets.Siege.Model.General.CellularSpace.Blocks;
using Assets.Siege.MonoBehaviors.CellableObjects;

namespace Assets.Siege.Model.General.CellularSpace.CellularSpaceGenerator.Interfaces
{
    public interface IBlockFromMonoConverter
    {
        public AbstractBlock Convert(CellableMono cellableMono);
    }
}