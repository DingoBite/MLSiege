using System;
using Assets.Siege.CellularSpace.Features;
using Assets.Siege.CellularSpace.General.Interfaces;
using Assets.Siege.CellularSpace.Repositories.Interfaces;
using Assets.Siege.View.General.MonoBehaviors;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Siege.CellularSpace.General.CellObjects
{
    public abstract class CellFrame<TSelf, TCellObj, TMono, TFeatures> : IToggle, IDisposable, IMovable
        where TSelf : CellFrame<TSelf, TCellObj, TMono, TFeatures>
        where TCellObj : CellObject<TFeatures>
        where TMono : ActableMono
        where TFeatures : AbstractFeatures
    {
        public readonly IFrameSpaceContext<TSelf> SelfSpace;
        protected readonly TCellObj _cellObj;
        protected readonly TMono _mono;

        protected CellFrame(IFrameSpaceContext<TSelf> selfSpace, TCellObj cellObj, TMono mono)
        {
            SelfSpace = selfSpace;
            _cellObj = cellObj;
            _mono = mono;
        }

        public int Id => _mono.Id;

        public Vector3Int Coords => SelfSpace[Id];

        public TFeatures Features => _cellObj.Features;

        public void Move(Vector3 position, Action atAnimationAction) => _mono.Move(position, atAnimationAction);

        public void Enable() => _mono.gameObject.SetActive(true);

        public void Disable() => _mono.gameObject.SetActive(false);

        public void Dispose() => Object.Destroy(_mono.gameObject);

        ~CellFrame() => Dispose();

        public override string ToString() => $"{Id}: {Coords}";
    }
}