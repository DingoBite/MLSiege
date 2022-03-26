using System.Collections.Generic;

namespace Game.Scripts.Time
{
    public class UpdateTicker : AbstractUpdateTicker
    {
        protected override void OnUpdate()
        {
            foreach (var updatable in _updatableRepository.GetValues())
            {
                updatable?.OnUpdate();
            }
        }
    }
}