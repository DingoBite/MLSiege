using Assets.Siege.Model.BlockSpace.GridShapers.Interfaces;
using Assets.Siege.View.General.MonoBehaviors;
using UnityEngine;

namespace Assets.Siege.Model.BlockSpace.Fabrics.Interfaces
{
    public interface IMonoFabric<TMono, in TData> where TMono : ActableMono
    {
        public void Init(ITilemapLevelsGrid<TMono> tilemapLevelsGrid);

        public TMono Make(int id, int level, Vector3 position, TData data);
    }
}