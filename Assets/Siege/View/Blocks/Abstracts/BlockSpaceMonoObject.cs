using Assets.Siege.View.General;
using UnityEngine;

namespace Assets.Siege.View.Blocks.Abstracts
{
    public abstract class BlockSpaceMonoObject<TInfo>: OperationalMono
    {
        [SerializeField] protected InfoScriptableObject<TInfo> _scriptableObjectInfo;

        private TInfo _info;

        public TInfo GetInfo() => _info ??= _scriptableObjectInfo.GetInfo();
    }
}