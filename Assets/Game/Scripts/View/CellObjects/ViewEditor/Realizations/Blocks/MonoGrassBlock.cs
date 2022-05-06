using Game.Scripts.CellularSpace.CellObjects.CellObjectCharacteristics;
using UnityEngine;

namespace Game.Scripts.View.CellObjects.Serialization.Realizations
{
    public class MonoGrassBlock : MonoBlock
    {
        [SerializeField] private bool _isModifiable;
        protected override bool IsModifiable => _isModifiable;
        protected override BlockCharacteristic Characteristics => new BlockCharacteristic(5, 5);
    }
}