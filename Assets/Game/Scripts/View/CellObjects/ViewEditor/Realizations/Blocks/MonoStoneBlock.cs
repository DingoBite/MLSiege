using Game.Scripts.CellularSpace.CellObjects.CellObjectCharacteristics;
using UnityEngine;

namespace Game.Scripts.View.CellObjects.Serialization.Realizations
{
    public class MonoStoneBlock : MonoBlock
    {
        [SerializeField] private bool _isModifiable;
        protected override bool IsModifiable => _isModifiable;
        protected override BlockCharacteristic Characteristics => new BlockCharacteristic(10, 10);
    }
}