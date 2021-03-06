using System;
using System.Collections.Generic;
using Game.Scripts.View.CellObjects;
using Game.Scripts.View.CellObjects.ViewMono;

namespace Game.Scripts.General.Interfaces
{
    public interface IPrefabsByKey<TKey, TPrefab> : IDictionary<TKey, TPrefab>
    where TKey : Enum
    where TPrefab : AbstractMonoCellObject
    {
    }
}