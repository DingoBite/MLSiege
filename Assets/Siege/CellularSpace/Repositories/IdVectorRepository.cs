using System.Collections.Generic;
using Assets.Siege.CellularSpace.General.Interfaces;
using UnityEngine;

namespace Assets.Siege.CellularSpace.Repositories
{
    public class IdVectorRepository: IRepository<int, Vector3Int>
    {
        private readonly Dictionary<int, Vector3Int> _idByCords;

        public IdVectorRepository()
        {
            _idByCords = new Dictionary<int, Vector3Int>();
        }

        public Vector3Int this[int id]
        {
            get => _idByCords[id];
            set => UpdateCustomer(id, value);
        }

        public bool ContainsKey(int id) => _idByCords.ContainsKey(id);

        private void UpdateCustomer(int id, Vector3Int coords)
        {
            if (!ContainsKey(id))
                _idByCords.Add(id, coords);
            else
                _idByCords[id] = coords;
        }

        public bool TryGetCustomer(int key, out Vector3Int customer) => _idByCords.TryGetValue(key, out customer);

        public void Save() => Debug.Log(_idByCords);

        public void Remove(int key) => _idByCords.Remove(key);

        public void Clear() => _idByCords.Clear();

        public IEnumerable<Vector3Int> GetCustomers() => _idByCords.Values;
    }
}