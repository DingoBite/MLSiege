using System;
using UnityEngine;

namespace Assets.Siege.CellularSpace.General.Interfaces
{
    public interface IMovable
    {
        public void Move(Vector3 position, Action atAnimationAction = null);
    }
}