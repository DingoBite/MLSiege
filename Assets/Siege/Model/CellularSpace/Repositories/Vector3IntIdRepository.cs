using System.Collections.Generic;
using Assets.Siege.Model.CellularSpace.Interfaces;
using UnityEngine;

namespace Assets.Siege.Model.CellularSpace.Repositories
{
    public class Vector3IntIdRepository: IIdRepository<Vector3Int>
    {
        private readonly Dictionary<Vector3Int, int> _idByCords;

        public Vector3IntIdRepository()
        {
            _idByCords = new Dictionary<Vector3Int, int>();
        }

        public int this[Vector3Int coords] => _idByCords[coords];

        public void Add(Vector3Int coords, int id) => _idByCords.Add(coords, id);

        public void Remove(Vector3Int coords) => _idByCords.Remove(coords);
        public void Clear() => _idByCords.Clear();
    }
}