using System.Collections.Generic;
using System.Linq;
using Assets.Siege.Model.BlockSpace.General.Interfaces;
using UnityEngine;

namespace Assets.Siege.Model.BlockSpace.Repositories
{
    public class VectorIdRepository: IRepository<Vector3Int, int>
    {
        private readonly Dictionary<Vector3Int, int> _idByCords;

        public VectorIdRepository()
        {
            _idByCords = new Dictionary<Vector3Int, int>();
        }

        public int this[Vector3Int id] {
            get => _idByCords[id];
            set => UpdateCustomer(id, value);
        }

        public bool ContainsKey(Vector3Int coords) => _idByCords.ContainsKey(coords);

        private void UpdateCustomer(Vector3Int coords, int id)
        {
            if (!ContainsKey(coords))
                _idByCords.Add(coords, id);
            else
                _idByCords[coords] = id;
        }

        public bool TryGetCustomer(Vector3Int key, out int customer) => _idByCords.TryGetValue(key, out customer);

        public void Save() => Debug.Log(_idByCords);

        public void Remove(Vector3Int key) => _idByCords.Remove(key);

        public void Clear() => _idByCords.Clear();

        public IEnumerable<int> GetCustomers() => _idByCords.Values;
    }
}