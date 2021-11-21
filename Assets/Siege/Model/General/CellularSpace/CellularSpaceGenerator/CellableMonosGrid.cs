using System.Collections.Generic;
using Assets.Siege.Model.General.Repositories.Interfaces;
using Assets.Siege.MonoBehaviors.CellableObjects;
using UnityEngine;
using Zenject;

namespace Assets.Siege.Model.General.CellularSpace.CellularSpaceGenerator
{
    public class CellableMonosGrid: IRepository<CellableMono>
    {
        [Inject] private ICellableGridConverter _cellableGridConverter;

        private readonly Dictionary<int, CellableMono> _cellableMonos;

        public CellableMonosGrid(Grid gameObjectsGrid)
        {
            _cellableMonos = _cellableGridConverter.CellableMonosFromGrid(gameObjectsGrid);
        }

        public IEnumerable<CellableMono> GetCustomers() => _cellableMonos.Values;

        public bool TryGetCustomerById(int id, out CellableMono customer) => _cellableMonos.TryGetValue(id, out customer);

        public int InsertCustomer(CellableMono customer)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCustomer(int id) => _cellableMonos.Remove(id);

        public bool TryUpdateCustomer(int id, CellableMono newCustomer)
        {
            if (!_cellableMonos.ContainsKey(id)) 
                return false;
            _cellableMonos[id] = newCustomer;
            return true;
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }
    }
}