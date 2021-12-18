﻿using System;
using System.Collections.Generic;
using Assets.Siege.View.General.MonoBehaviors;


namespace Assets.Siege.Model.BlockSpace.General.Interfaces
{
    public interface IPrefabsByType<TKey, TPrefab> : IDictionary<TKey, TPrefab>
    where TPrefab : ActableMono
    where TKey : Enum
    {
    }
}