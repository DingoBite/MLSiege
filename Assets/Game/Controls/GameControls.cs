// GENERATED AUTOMATICALLY FROM 'Assets/Game/Controls/GameControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @GameControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameControls"",
    ""maps"": [
        {
            ""name"": ""Agent"",
            ""id"": ""72dea70b-30ac-4137-8417-0341895ec824"",
            ""actions"": [
                {
                    ""name"": ""Gravity"",
                    ""type"": ""Button"",
                    ""id"": ""d1e403d1-bff6-43d0-8727-cf1a68361906"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ec8911fc-5d06-4f9c-a5ec-dd4d6070968a"",
                    ""path"": ""<Keyboard>/#(G)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Gravity"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Agent
        m_Agent = asset.FindActionMap("Agent", throwIfNotFound: true);
        m_Agent_Gravity = m_Agent.FindAction("Gravity", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Agent
    private readonly InputActionMap m_Agent;
    private IAgentActions m_AgentActionsCallbackInterface;
    private readonly InputAction m_Agent_Gravity;
    public struct AgentActions
    {
        private @GameControls m_Wrapper;
        public AgentActions(@GameControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Gravity => m_Wrapper.m_Agent_Gravity;
        public InputActionMap Get() { return m_Wrapper.m_Agent; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AgentActions set) { return set.Get(); }
        public void SetCallbacks(IAgentActions instance)
        {
            if (m_Wrapper.m_AgentActionsCallbackInterface != null)
            {
                @Gravity.started -= m_Wrapper.m_AgentActionsCallbackInterface.OnGravity;
                @Gravity.performed -= m_Wrapper.m_AgentActionsCallbackInterface.OnGravity;
                @Gravity.canceled -= m_Wrapper.m_AgentActionsCallbackInterface.OnGravity;
            }
            m_Wrapper.m_AgentActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Gravity.started += instance.OnGravity;
                @Gravity.performed += instance.OnGravity;
                @Gravity.canceled += instance.OnGravity;
            }
        }
    }
    public AgentActions @Agent => new AgentActions(this);
    public interface IAgentActions
    {
        void OnGravity(InputAction.CallbackContext context);
    }
}
