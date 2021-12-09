using System;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.Model.General.Interfaces;
using Assets.Siege.View.Blocks.Abstracts;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Siege.Model.General.BlockSpaceObjects
{
    public abstract class BlockObjectFrame<TSelf, TData, TMono, TFeatures> : IToggle, IDisposable
        where TMono : OperationalMono
        where TSelf : BlockObjectFrame<TSelf, TData, TMono, TFeatures>
    {
        protected readonly IFrameSpaceContext<TSelf> _frameSpace;
        protected readonly TData _data;
        protected readonly TMono _mono;

        protected BlockObjectFrame(IFrameSpaceContext<TSelf> frameSpace,
            TData data, TMono mono, Vector3Int coords)
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

        ~BlockObjectFrame() => Dispose();
    }
}