using System;
using Assets.Siege.View.General.ScriptableObjects;
using UnityEngine;

namespace Assets.Siege.View.General.MonoBehaviors
{
    public abstract class FrameSpaceMonoObject<TInfo> : ActableMono
    {
        [SerializeField] public ActableScriptableObject<TInfo> _actableScriptableObject;

        private TInfo _info;

        public TInfo GetInfo() => _info ??= _actableScriptableObject.GetInfo();

        public override void Move(Vector3 position, Action postAnimationAction = null)
        {
            if (!ReadyToMove) return;
            ReadyToMove = false;
            _actableScriptableObject.Move(this, position, () =>
            {
                postAnimationAction?.Invoke();
                ReadyToMove = true;
            });
        }

        public override void Act<T>(T actType, Action postAnimationAction = null)
        {
            if (!ReadyToAct) return;
            ReadyToAct = false;
            _actableScriptableObject.Act(this, actType, () =>
            {
                postAnimationAction?.Invoke();
                ReadyToAct = true;
            });
        }
    }
}