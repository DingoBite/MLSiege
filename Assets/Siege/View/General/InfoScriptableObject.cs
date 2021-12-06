using UnityEngine;

namespace Assets.Siege.View.General
{
    public abstract class InfoScriptableObject<TInfo> : ScriptableObject
    {
        public abstract TInfo GetInfo();
    }
}