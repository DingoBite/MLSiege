using Assets.Siege.View.General.MonoBehaviors;
using UnityEngine;

namespace Assets.Siege.Model.BlockSpace.Fabrics.Interfaces
{
    public interface IMonoFabric<out TMono, in TData> where TMono : ActableMono
    {
        public TMono Make(int id, int level, Vector3 position, TData data);
    }
}