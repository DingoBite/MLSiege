using System;
using UnityEngine;

namespace Assets.Siege.View.General.MonoBehaviors
{
    public abstract class ActableMono: MonoBehaviour
    {
        protected bool ReadyToMove { get; set; } = true;
        protected bool ReadyToAct { get; set; } = true;

        public int Id
        {
            get => _id;
            set
            {
                if (_id >= 0)
                    throw new Exception("Try to reinit ActableMono Id");
                if (value < 0)
                    throw new Exception("ActableMono id bellow zero");
                _id = value;
            }
        }

        private int _id = -1;

        public abstract void Move(Vector3 position, Action postAnimationAction = null);
        public abstract void Act<T>(T actType, Action postAnimationAction = null) where T: Enum;
    }
}