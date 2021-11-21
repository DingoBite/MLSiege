using System.Collections.Generic;
using Assets.Siege.Model.General.CellularSpace.Blocks;
using Assets.Siege.Model.General.Repositories.Interfaces;

namespace Assets.Siege.Model.General.CellularSpace.Interfaces
{
    public interface ICellularContext: ICellularSpace
    {
        public IEnumerable<AbstractBlock> GetCustomers();
        public bool TryGetCustomerById(int id, out AbstractBlock customer);
    }
}