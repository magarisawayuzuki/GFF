// GENERATED AUTOMATICALLY FROM 'Assets/nanaumiForWork/Script_Nanaumi/InputController.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputController : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputController"",
    ""maps"": [
        {
            ""name"": ""UI"",
            ""id"": ""de99001f-5529-492b-9b89-058a17c43f88"",
            ""actions"": [
                {
                    ""name"": ""Decide"",
                    ""type"": ""Button"",
                    ""id"": ""14337921-bf31-4631-813b-9083865394db"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select_Horizontal"",
                    ""type"": ""Button"",
                    ""id"": ""ec5f695a-6d72-4a9c-9f9b-f02cb2478856"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select_Vertical"",
                    ""type"": ""Button"",
                    ""id"": ""85e73181-9638-4446-9ade-0e0a32529ed4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""30049d82-870a-4575-bfb8-b3ae7c8836bf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a5367f81-23de-4888-8b69-b558d7639b0d"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Decide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""59e62257-dd3d-4846-8069-2af8bbb89d64"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select_Horizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""9280d6e5-1565-442b-bfc8-38e48576fd3c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select_Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""06223255-b233-4de0-b23b-b1876af167e3"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select_Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""80d6fbb2-df81-46f0-a19e-716eef269e68"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select_Vertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""9e037471-933a-48ff-8bc9-93bfaa395084"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select_Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""40ebdd2a-b528-4050-b023-9ed0135780c8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select_Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""12e6eed7-d99b-4e9d-ae5e-ad2451768319"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Decide = m_UI.FindAction("Decide", throwIfNotFound: true);
        m_UI_Select_Horizontal = m_UI.FindAction("Select_Horizontal", throwIfNotFound: true);
        m_UI_Select_Vertical = m_UI.FindAction("Select_Vertical", throwIfNotFound: true);
        m_UI_Pause = m_UI.FindAction("Pause", throwIfNotFound: true);
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

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Decide;
    private readonly InputAction m_UI_Select_Horizontal;
    private readonly InputAction m_UI_Select_Vertical;
    private readonly InputAction m_UI_Pause;
    public struct UIActions
    {
        private @InputController m_Wrapper;
        public UIActions(@InputController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Decide => m_Wrapper.m_UI_Decide;
        public InputAction @Select_Horizontal => m_Wrapper.m_UI_Select_Horizontal;
        public InputAction @Select_Vertical => m_Wrapper.m_UI_Select_Vertical;
        public InputAction @Pause => m_Wrapper.m_UI_Pause;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Decide.started -= m_Wrapper.m_UIActionsCallbackInterface.OnDecide;
                @Decide.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnDecide;
                @Decide.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnDecide;
                @Select_Horizontal.started -= m_Wrapper.m_UIActionsCallbackInterface.OnSelect_Horizontal;
                @Select_Horizontal.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnSelect_Horizontal;
                @Select_Horizontal.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnSelect_Horizontal;
                @Select_Vertical.started -= m_Wrapper.m_UIActionsCallbackInterface.OnSelect_Vertical;
                @Select_Vertical.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnSelect_Vertical;
                @Select_Vertical.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnSelect_Vertical;
                @Pause.started -= m_Wrapper.m_UIActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Decide.started += instance.OnDecide;
                @Decide.performed += instance.OnDecide;
                @Decide.canceled += instance.OnDecide;
                @Select_Horizontal.started += instance.OnSelect_Horizontal;
                @Select_Horizontal.performed += instance.OnSelect_Horizontal;
                @Select_Horizontal.canceled += instance.OnSelect_Horizontal;
                @Select_Vertical.started += instance.OnSelect_Vertical;
                @Select_Vertical.performed += instance.OnSelect_Vertical;
                @Select_Vertical.canceled += instance.OnSelect_Vertical;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    public interface IUIActions
    {
        void OnDecide(InputAction.CallbackContext context);
        void OnSelect_Horizontal(InputAction.CallbackContext context);
        void OnSelect_Vertical(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
