using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Siege.ScriptableObjects
{
    public abstract class InfoScriptableObject<T> : ScriptableObject where T : Enum
    {
        public abstract Tuple<T, IEnumerable<int>> GetInfo();
    }
}