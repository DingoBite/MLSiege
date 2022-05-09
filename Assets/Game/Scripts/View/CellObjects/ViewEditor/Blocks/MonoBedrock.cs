using Game.Scripts.CellObjects.CellObjectCharacteristics;
using UnityEngine;

namespace Game.Scripts.View.CellObjects.ViewEditor.Blocks
{
    public class MonoBedrock : MonoBlock
    {
        [SerializeField] private bool _isModifiable;
        protected override bool IsModifiable => _isModifiable;
        protected override BlockCharacteristic Characteristics => new BlockCharacteristic(100, 100);
    }
}