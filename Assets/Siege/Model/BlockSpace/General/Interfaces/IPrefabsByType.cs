using System;
using System.Collections.Generic;
using Assets.Siege.View.General.MonoBehaviors;


namespace Assets.Siege.Model.BlockSpace.General.Interfaces
{
    public interface IPrefabsByType<TType, TMono> : IDictionary<TType, TMono>
    where TMono : ActableMono
    where TType : Enum
    {
    }
}