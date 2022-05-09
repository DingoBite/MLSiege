using System;
using Game.Scripts.View.CellObjects;
using Game.Scripts.View.CellObjects.ViewMono;

namespace Game.Scripts.Controls.InputControllers.MousePicker
{
    public class CellObjectMousePicker : AbstractActableMousePicker<AbstractMonoCellObject>
    {
        private readonly Action<int> _onMousePickAction;

        public CellObjectMousePicker(Action<int> onMousePickAction)
        {
            _onMousePickAction = onMousePickAction;
        }

        protected override void OnMousePick(int id)
        {
            if (id == -1) return;
            _onMousePickAction?.Invoke(id);
        }
    }
}