﻿using UnityEngine;

namespace Assets.Siege.Model.BlockSpace.CoordsConverters.Interfaces
{
    public interface IGridCoordsConverter
    {
        public Vector3Int Convert(Vector3 coords);
        public Vector3 Convert(Vector3Int coords);
    }
}