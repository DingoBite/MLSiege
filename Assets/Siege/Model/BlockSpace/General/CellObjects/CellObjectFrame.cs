using System;
using Assets.Siege.Model.BlockSpace.General.Interfaces;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.General.MonoBehaviors;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Siege.Model.BlockSpace.General.CellObjects
{
    public abstract class CellObjectFrame<TSelf, TCellObj, TMono, TFeatures> : IToggle, IDisposable
        where TSelf : CellObjectFrame<TSelf, TCellObj, TMono, TFeatures>
        where TCellObj : CellObject<TFeatures>
        where TMono : ActableMono
    {
        protected readonly IFrameSpaceContext<TSelf> _frameSpace;
        protected readonly TCellObj _data;
        protected readonly TMono _mono;

        protected CellObjectFrame(IFrameSpaceContext<TSelf> frameSpace,
            TCellObj data, TMono mono, Vector3Int coords)
        {
            _frameSpace = frameSpace;
            _data = data;
            _mono = mono;
            Coords = coords;
        }

        public int Id => _mono.Id;

        public abstract TFeatures Features { get; }

        public Vector3Int Coords
        {
            get => _coords;
            set => _mono.Move(_frameSpace.Convert(value), () => _coords = value);
        }

        private Vector3Int _coords;

        public void SwapPosition(TSelf packObject)
        {
            var tempPos = packObject.Coords;
            packObject.Coords = Coords;
            Coords = tempPos;
        }

        public void Enable() => _mono.gameObject.SetActive(true);

        public void Disable() => _mono.gameObject.SetActive(false);

        public void Dispose() => Object.Destroy(_mono.gameObject);

        ~CellObjectFrame() => Dispose();

        public override string ToString() => $"{Id}: {Coords}";
    }
}