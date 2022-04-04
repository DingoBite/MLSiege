using System;
using System.Collections.Generic;
using Game.Scripts.View.CellObjects;

namespace Game.Scripts.CellularSpace.General.Interfaces
{
    public interface IPrefabsByKey<TKey, TPrefab> : IDictionary<TKey, TPrefab>
    where TKey : Enum
    where TPrefab : AbstractMonoCellObject
    {
    }
}