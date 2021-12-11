using System;
using Assets.Siege.View.General.MonoBehaviors;
using UnityEngine;

namespace Assets.Siege.View.General.ScriptableObjects
{
    public abstract class ActableScriptableObject<TInfo> : ScriptableObject
    { 
        public abstract TInfo GetInfo();
        public abstract void Move(ActableMono self, Vector3 position, Action postAnimationAction = null);

        public abstract void Act<T>(ActableMono self, T actType, Action postAnimationAction = null) where T : Enum;
    }
}