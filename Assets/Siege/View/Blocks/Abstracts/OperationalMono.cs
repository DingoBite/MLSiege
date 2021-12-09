using System;
using UnityEngine;

namespace Assets.Siege.View.Blocks.Abstracts
{
    public abstract class OperationalMono: MonoBehaviour
    {
        public virtual int Id
        {
            get => _id;
            set
            {
                if (_id >= 0)
                    throw new Exception("Try to reinit MonoBlock Id");
                if (value < 0)
                    throw new Exception("MonoBlock id bellow zero");
                _id = value;
            }
        }

        private int _id = -1;

        public abstract void Move(Vector3 position, Action postAnimationAction = null);
        public abstract void Act<T>(T actType, Action postAnimationAction = null) where T: Enum;
    }
}