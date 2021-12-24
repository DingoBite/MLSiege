﻿using UnityEngine;

namespace Assets.Siege.CellularSpace.Repositories.Interfaces
{
    public interface IFrameSpaceCoordsController
    {
        public void Swap(int id1, int id2);
        public void Swap(Vector3Int coords1, Vector3Int coords2);
        public void MoveTo(Vector3Int newCoords, int id);
        public void MoveTo(Vector3 newPosition, int id);
        public void MoveTo(Vector3Int newCoords, Vector3Int coords);
        public void MoveTo(Vector3 newPosition, Vector3Int coords);
    }
}