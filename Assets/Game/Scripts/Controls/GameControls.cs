// GENERATED AUTOMATICALLY FROM 'Assets/Game/Scripts/Controls/GameControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Game.Scripts.Controls
{
    public class @GameControls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @GameControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameControls"",
    ""maps"": [
        {
            ""name"": ""World"",
            ""id"": ""72dea70b-30ac-4137-8417-0341895ec824"",
            ""actions"": [
                {
                    ""name"": ""GlobalAction"",
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
                    ""action"": ""GlobalAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Agent"",
            ""id"": ""5aeab9f8-af92-4567-a89c-f0dca68ba7fb"",
            ""actions"": [],
            ""bindings"": []
        },
        {
            ""name"": ""CellObject"",
            ""id"": ""41ac191e-0d3f-4fe5-8d46-715dec3f6d5d"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""c4b89487-4666-4a6d-a98f-a4ad312a21b5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD Keys"",
                    ""id"": ""4c73b13c-d9a0-45c9-a2f9-47d1c158626e"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c516f3dd-656d-4c25-a80e-18036214c341"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a48852e5-3d15-4b1c-b256-c7aff98eee33"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4115e6ca-3332-47d0-9d6a-4007a89c9c29"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f2c99462-e57f-40f1-95a3-ef975ac7665a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows Keys"",
                    ""id"": ""a63847ee-b436-4256-a4e5-f53d8b432417"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""54eee88f-eb1f-46de-8ec3-66a289475ca1"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""22ba91b7-e4b5-4e2a-8f1a-7a3054be2ced"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e4fbe16d-3174-4ca2-aade-a22b26bd4aa6"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""bd085e38-012e-4b33-b5c3-6b6e8fdd94cd"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Block"",
            ""id"": ""410bbb41-2919-43cb-9474-69c301140ba2"",
            ""actions"": [],
            ""bindings"": []
        },
        {
            ""name"": ""ObjectPicker"",
            ""id"": ""83f7867b-6c6f-48a2-89da-355f71da0f7c"",
            ""actions"": [
                {
                    ""name"": ""SelectObject"",
                    ""type"": ""Button"",
                    ""id"": ""b37c15b9-7170-42c3-ac83-e229e4081eee"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fb6817c8-8da7-46e3-85a6-f4b34ca3a6d0"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectObject"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // World
            m_World = asset.FindActionMap("World", throwIfNotFound: true);
            m_World_GlobalAction = m_World.FindAction("GlobalAction", throwIfNotFound: true);
            // Agent
            m_Agent = asset.FindActionMap("Agent", throwIfNotFound: true);
            // CellObject
            m_CellObject = asset.FindActionMap("CellObject", throwIfNotFound: true);
            m_CellObject_Movement = m_CellObject.FindAction("Movement", throwIfNotFound: true);
            // Block
            m_Block = asset.FindActionMap("Block", throwIfNotFound: true);
            // ObjectPicker
            m_ObjectPicker = asset.FindActionMap("ObjectPicker", throwIfNotFound: true);
            m_ObjectPicker_SelectObject = m_ObjectPicker.FindAction("SelectObject", throwIfNotFound: true);
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

        // World
        private readonly InputActionMap m_World;
        private IWorldActions m_WorldActionsCallbackInterface;
        private readonly InputAction m_World_GlobalAction;
        public struct WorldActions
        {
            private @GameControls m_Wrapper;
            public WorldActions(@GameControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @GlobalAction => m_Wrapper.m_World_GlobalAction;
            public InputActionMap Get() { return m_Wrapper.m_World; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(WorldActions set) { return set.Get(); }
            public void SetCallbacks(IWorldActions instance)
            {
                if (m_Wrapper.m_WorldActionsCallbackInterface != null)
                {
                    @GlobalAction.started -= m_Wrapper.m_WorldActionsCallbackInterface.OnGlobalAction;
                    @GlobalAction.performed -= m_Wrapper.m_WorldActionsCallbackInterface.OnGlobalAction;
                    @GlobalAction.canceled -= m_Wrapper.m_WorldActionsCallbackInterface.OnGlobalAction;
                }
                m_Wrapper.m_WorldActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @GlobalAction.started += instance.OnGlobalAction;
                    @GlobalAction.performed += instance.OnGlobalAction;
                    @GlobalAction.canceled += instance.OnGlobalAction;
                }
            }
        }
        public WorldActions @World => new WorldActions(this);

        // Agent
        private readonly InputActionMap m_Agent;
        private IAgentActions m_AgentActionsCallbackInterface;
        public struct AgentActions
        {
            private @GameControls m_Wrapper;
            public AgentActions(@GameControls wrapper) { m_Wrapper = wrapper; }
            public InputActionMap Get() { return m_Wrapper.m_Agent; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(AgentActions set) { return set.Get(); }
            public void SetCallbacks(IAgentActions instance)
            {
                if (m_Wrapper.m_AgentActionsCallbackInterface != null)
                {
                }
                m_Wrapper.m_AgentActionsCallbackInterface = instance;
                if (instance != null)
                {
                }
            }
        }
        public AgentActions @Agent => new AgentActions(this);

        // CellObject
        private readonly InputActionMap m_CellObject;
        private ICellObjectActions m_CellObjectActionsCallbackInterface;
        private readonly InputAction m_CellObject_Movement;
        public struct CellObjectActions
        {
            private @GameControls m_Wrapper;
            public CellObjectActions(@GameControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Movement => m_Wrapper.m_CellObject_Movement;
            public InputActionMap Get() { return m_Wrapper.m_CellObject; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(CellObjectActions set) { return set.Get(); }
            public void SetCallbacks(ICellObjectActions instance)
            {
                if (m_Wrapper.m_CellObjectActionsCallbackInterface != null)
                {
                    @Movement.started -= m_Wrapper.m_CellObjectActionsCallbackInterface.OnMovement;
                    @Movement.performed -= m_Wrapper.m_CellObjectActionsCallbackInterface.OnMovement;
                    @Movement.canceled -= m_Wrapper.m_CellObjectActionsCallbackInterface.OnMovement;
                }
                m_Wrapper.m_CellObjectActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Movement.started += instance.OnMovement;
                    @Movement.performed += instance.OnMovement;
                    @Movement.canceled += instance.OnMovement;
                }
            }
        }
        public CellObjectActions @CellObject => new CellObjectActions(this);

        // Block
        private readonly InputActionMap m_Block;
        private IBlockActions m_BlockActionsCallbackInterface;
        public struct BlockActions
        {
            private @GameControls m_Wrapper;
            public BlockActions(@GameControls wrapper) { m_Wrapper = wrapper; }
            public InputActionMap Get() { return m_Wrapper.m_Block; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(BlockActions set) { return set.Get(); }
            public void SetCallbacks(IBlockActions instance)
            {
                if (m_Wrapper.m_BlockActionsCallbackInterface != null)
                {
                }
                m_Wrapper.m_BlockActionsCallbackInterface = instance;
                if (instance != null)
                {
                }
            }
        }
        public BlockActions @Block => new BlockActions(this);

        // ObjectPicker
        private readonly InputActionMap m_ObjectPicker;
        private IObjectPickerActions m_ObjectPickerActionsCallbackInterface;
        private readonly InputAction m_ObjectPicker_SelectObject;
        public struct ObjectPickerActions
        {
            private @GameControls m_Wrapper;
            public ObjectPickerActions(@GameControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @SelectObject => m_Wrapper.m_ObjectPicker_SelectObject;
            public InputActionMap Get() { return m_Wrapper.m_ObjectPicker; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(ObjectPickerActions set) { return set.Get(); }
            public void SetCallbacks(IObjectPickerActions instance)
            {
                if (m_Wrapper.m_ObjectPickerActionsCallbackInterface != null)
                {
                    @SelectObject.started -= m_Wrapper.m_ObjectPickerActionsCallbackInterface.OnSelectObject;
                    @SelectObject.performed -= m_Wrapper.m_ObjectPickerActionsCallbackInterface.OnSelectObject;
                    @SelectObject.canceled -= m_Wrapper.m_ObjectPickerActionsCallbackInterface.OnSelectObject;
                }
                m_Wrapper.m_ObjectPickerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @SelectObject.started += instance.OnSelectObject;
                    @SelectObject.performed += instance.OnSelectObject;
                    @SelectObject.canceled += instance.OnSelectObject;
                }
            }
        }
        public ObjectPickerActions @ObjectPicker => new ObjectPickerActions(this);
        public interface IWorldActions
        {
            void OnGlobalAction(InputAction.CallbackContext context);
        }
        public interface IAgentActions
        {
        }
        public interface ICellObjectActions
        {
            void OnMovement(InputAction.CallbackContext context);
        }
        public interface IBlockActions
        {
        }
        public interface IObjectPickerActions
        {
            void OnSelectObject(InputAction.CallbackContext context);
        }
    }
}
