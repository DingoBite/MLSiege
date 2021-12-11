using System.Collections.Generic;
using Assets.Siege.Model.BlockSpace.General.Interfaces;
using UnityEngine;

namespace Assets.Siege.Model.BlockSpace.Repositories
{
    public class Vector3IntIdRepository: IIdRepository<Vector3Int>
    {
        private readonly Dictionary<Vector3Int, int> _idByCords;

        public Vector3IntIdRepository()
        {
            _idByCords = new Dictionary<Vector3Int, int>();
        }

        public int this[Vector3Int coords] {
            get => _idByCords[coords];
            set => Update(coords, value);
        }

        private void Update(Vector3Int coords, int id)
        {
            if (!ContainsKey(coords))
                _idByCords.Add(coords, id);
            else
                _idByCords[coords] = id;
        }

        public bool ContainsKey(Vector3Int coords) => _idByCords.ContainsKey(coords);
        public void Remove(Vector3Int coords) => _idByCords.Remove(coords);
        public void Clear() => _idByCords.Clear();
    }
}