using Game.Scripts.CellularSpace.CellObjects.CellObjectCharacteristics;
using UnityEngine;

namespace Game.Scripts.View.CellObjects.Serialization.Realizations
{
    public class MonoBedrock : MonoBlock
    {
        [SerializeField] private bool _isModifiable;
        protected override bool IsModifiable => _isModifiable;
        protected override BlockCharacteristic Characteristics => new BlockCharacteristic(100, 100);
    }
}