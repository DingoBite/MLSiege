using Game.Scripts.Time.Interfaces;
using Game.Scripts.View.InputControllers.KeyHandler;
using UnityEngine;

namespace Game.Scripts.View.InputControllers
{
    public class WASDSSHandler : IUpdatable
    {
        public readonly KeyInputHandler WInputHandler = new KeyInputHandler(KeyCode.W);
        public readonly KeyInputHandler AInputHandler = new KeyInputHandler(KeyCode.A);
        public readonly KeyInputHandler SInputHandler = new KeyInputHandler(KeyCode.S);
        public readonly KeyInputHandler DInputHandler = new KeyInputHandler(KeyCode.D);
        public readonly KeyInputHandler SpaceInputHandler = new KeyInputHandler(KeyCode.Space);
        public readonly KeyInputHandler ShiftInputHandler = new KeyInputHandler(KeyCode.LeftShift);
        
        public void OnUpdate()
        {
            WInputHandler.OnUpdate();
            AInputHandler.OnUpdate();
            SInputHandler.OnUpdate();
            DInputHandler.OnUpdate();
            SpaceInputHandler.OnUpdate();
            ShiftInputHandler.OnUpdate();
        }
    }
}