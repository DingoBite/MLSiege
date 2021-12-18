using System;
using Assets.Siege.Model.BlockSpace.Features;
using Assets.Siege.Model.BlockSpace.General.Interfaces;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.General.MonoBehaviors;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Siege.Model.BlockSpace.General.CellObjects
{
    public abstract class CellObjectFrame<TSelf, TCellObj, TMono, TFeatures> : IToggle, IDisposable, IMovable
        where TSelf : CellObjectFrame<TSelf, TCellObj, TMono, TFeatures>
        where TCellObj : CellObject<TFeatures>
        where TMono : ActableMono
        where TFeatures : AbstractFeatures
    {
        public readonly IFrameSpaceContext<TSelf> SelfSpace;
        protected readonly TCellObj _cellObj;
        protected readonly TMono _mono;

        protected CellObjectFrame(IFrameSpaceContext<TSelf> selfSpace, TCellObj cellObj, TMono mono)
        {
            SelfSpace = selfSpace;
            _cellObj = cellObj;
            _mono = mono;
        }

        public int Id => _mono.Id;

        public Vector3Int Coords => SelfSpace[Id];

        public TFeatures Features => _cellObj.Features;

        public void Move(Vector3 position, Action postAnimationAction) => _mono.Move(position, postAnimationAction);

        public void Enable() => _mono.gameObject.SetActive(true);

        public void Disable() => _mono.gameObject.SetActive(false);

        public void Dispose() => Object.Destroy(_mono.gameObject);

        ~CellObjectFrame() => Dispose();

        public override string ToString() => $"{Id}: {Coords}";
    }
}