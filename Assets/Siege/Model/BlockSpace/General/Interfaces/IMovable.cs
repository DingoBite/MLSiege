using System;
using UnityEngine;

namespace Assets.Siege.Model.BlockSpace.General.CellObjects
{
    public interface IMovable
    {
        public void Move(Vector3 position, Action postAnimationAction = null);
    }
}