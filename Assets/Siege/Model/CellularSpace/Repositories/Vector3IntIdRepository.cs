using System.Collections.Generic;
using Assets.Siege.Model.General.Interfaces;
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

        public bool Add(Vector3Int coords, int id)
        {
            if (ContainsKey(coords))
                return false;
            _idByCords.Add(coords, id);
            return true;
        }

        public bool ContainsKey(Vector3Int coords) => _idByCords.ContainsKey(coords);
        public void Remove(Vector3Int coords) => _idByCords.Remove(coords);
        public void Clear() => _idByCords.Clear();
    }
}