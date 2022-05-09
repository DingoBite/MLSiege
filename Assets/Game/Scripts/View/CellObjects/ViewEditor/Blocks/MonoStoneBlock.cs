using Game.Scripts.CellObjects.CellObjectCharacteristics;
using UnityEngine;

namespace Game.Scripts.View.CellObjects.ViewEditor.Blocks
{
    public class MonoStoneBlock : MonoBlock
    {
        [SerializeField] private int _maxDurability;

        [SerializeField] private bool _isModifiable;
        protected override bool IsModifiable => _isModifiable;
        protected override BlockCharacteristic Characteristics => new BlockCharacteristic(_maxDurability, _maxDurability);
        
        private void OnValidate()
        {
            if (_maxDurability < 0)
                _maxDurability = 0;
            else if (_maxDurability > 100)
                _maxDurability = 100;
        }
    }
}