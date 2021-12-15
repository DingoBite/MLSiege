using System;
using Assets.Siege.Model.BlockSpace.Features;
using Assets.Siege.Model.BlockSpace.General.Interfaces;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.General.MonoBehaviors;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Siege.Model.BlockSpace.General.CellObjects
{
    public abstract class CellObjectFrame<TSelf, TCellObj, TMono, TFeatures> : IToggle, IDisposable, ISpaceLocated
        where TSelf : CellObjectFrame<TSelf, TCellObj, TMono, TFeatures>
        where TCellObj : CellObject<TFeatures>
        where TMono : ActableMono
        where TFeatures : AbstractFeatures
    {
        protected readonly IFrameSpaceContext<TSelf> _context;
        protected readonly TCellObj _cellObj;
        private readonly TMono _mono;

        protected CellObjectFrame(IFrameSpaceContext<TSelf> context,
            TCellObj cellObj, TMono mono, Vector3Int coords)
        {
            _context = context;
            _cellObj = cellObj;
            _mono = mono;
            Coords = coords;
        }
        public int Id => _mono.Id;

        public TFeatures Features => _cellObj.Features;

        public Vector3Int Coords
        {
            get => _coords;
            private set => _context.MoveTo(value, Id);
        }

        private Vector3Int _coords;

        public void HardSetCoords(Vector3Int newCoords)
        {
            _mono.Move(newCoords, () => _coords = newCoords);
        }

        public void SwapPosition(ISpaceLocated packObject)
        {
            var tempPos = packObject.Coords;
            _context.MoveTo(Coords, packObject.Id);
            Coords = tempPos;
        }

        protected void PostAnimationCommit<T>(T action, Action commitAction) where T : Enum =>
            _mono.Act(action, commitAction);

        public void Enable() => _mono.gameObject.SetActive(true);

        public void Disable() => _mono.gameObject.SetActive(false);

        public void Dispose() => Object.Destroy(_mono.gameObject);

        ~CellObjectFrame() => Dispose();

        public override string ToString() => $"{Id}: {Coords}";
    }
}