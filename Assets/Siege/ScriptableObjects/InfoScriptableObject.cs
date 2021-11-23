using UnityEngine;

namespace Assets.Siege.ScriptableObjects
{
    public abstract class InfoScriptableObject<TInfo> : ScriptableObject
    {
        public abstract TInfo GetInfo();
    }
}