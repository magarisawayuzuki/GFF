// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Controller/Input/InputSystem.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputSystem : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputSystem()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputSystem"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""d1547179-8b27-4f8b-b224-00300e35607e"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""23a689c8-86be-47a0-8750-f0454cef4bcc"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Value"",
                    ""id"": ""670e1759-8898-4520-8f84-720f4852b77e"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwordAttack"",
                    ""type"": ""Button"",
                    ""id"": ""e3c0ef49-53e3-4fe3-a285-47ae15a4b0de"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""HammerAttack"",
                    ""type"": ""Button"",
                    ""id"": ""892d516a-265b-46a5-b5f9-124e6b48bd72"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Test"",
                    ""type"": ""Button"",
                    ""id"": ""b10c0ef4-266c-4c22-8a9f-91e7fce694b6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""SlowTap(duration=1)""
                },
                {
                    ""name"": ""Invincible"",
                    ""type"": ""Button"",
                    ""id"": ""76a67bf2-ebe8-4978-a8ca-c4d05538b34b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""SlowTap(duration=1)""
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Value"",
                    ""id"": ""46500c50-36b2-42e0-9748-0a80ab6733a2"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""3a6a8cae-3745-4795-b025-661f75cc9a99"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""306d5f86-d4e4-4a77-b26e-0a767acbaf9d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""834a3df2-26be-4339-8fe6-31655910b396"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""XboxGamepad"",
                    ""id"": ""20b468f3-ff53-4a9a-9299-7d39b7018c2c"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""7bc37f99-c841-4454-8e01-22887de5f73c"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""9b9c68fb-bf64-47a1-815f-7a4630d8f049"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c37dea96-79b4-4fc6-8e0a-41e77999c42d"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0d4f8d6a-6b9f-47c3-a3f6-b81001b0bd2b"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""caa51d9b-b9cd-42a1-9a42-86e2bb1019e9"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SwordAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""11eb020e-5cee-40bf-b5e4-3275ae16e4df"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SwordAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b2d15d78-a8c9-49f7-8092-51b28b50ca5a"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""HammerAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""db707418-b8d1-469c-8343-dff5e587e49d"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""HammerAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0e1fbe9f-2f5b-4e46-8a15-22b1971d2085"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2b722b73-ce8e-467c-ad13-514a19c1cf71"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Invincible"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""8a51dde2-c3a1-4d98-b657-58f174d201c4"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Invincible"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""button"",
                    ""id"": ""bac85ad1-0645-4785-8885-ed9807c9e2d8"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Invincible"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""927152d6-c40c-4a1f-a7dc-78aba30021fd"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Invincible"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a78ea256-2c24-47d1-bae7-f8c9395c91a6"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e7b143da-47cd-4ceb-a7c2-09ace590ddc3"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_SwordAttack = m_Player.FindAction("SwordAttack", throwIfNotFound: true);
        m_Player_HammerAttack = m_Player.FindAction("HammerAttack", throwIfNotFound: true);
        m_Player_Test = m_Player.FindAction("Test", throwIfNotFound: true);
        m_Player_Invincible = m_Player.FindAction("Invincible", throwIfNotFound: true);
        m_Player_Down = m_Player.FindAction("Down", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_SwordAttack;
    private readonly InputAction m_Player_HammerAttack;
    private readonly InputAction m_Player_Test;
    private readonly InputAction m_Player_Invincible;
    private readonly InputAction m_Player_Down;
    public struct PlayerActions
    {
        private @InputSystem m_Wrapper;
        public PlayerActions(@InputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @SwordAttack => m_Wrapper.m_Player_SwordAttack;
        public InputAction @HammerAttack => m_Wrapper.m_Player_HammerAttack;
        public InputAction @Test => m_Wrapper.m_Player_Test;
        public InputAction @Invincible => m_Wrapper.m_Player_Invincible;
        public InputAction @Down => m_Wrapper.m_Player_Down;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @SwordAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwordAttack;
                @SwordAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwordAttack;
                @SwordAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwordAttack;
                @HammerAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHammerAttack;
                @HammerAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHammerAttack;
                @HammerAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHammerAttack;
                @Test.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTest;
                @Test.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTest;
                @Test.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTest;
                @Invincible.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInvincible;
                @Invincible.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInvincible;
                @Invincible.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInvincible;
                @Down.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDown;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @SwordAttack.started += instance.OnSwordAttack;
                @SwordAttack.performed += instance.OnSwordAttack;
                @SwordAttack.canceled += instance.OnSwordAttack;
                @HammerAttack.started += instance.OnHammerAttack;
                @HammerAttack.performed += instance.OnHammerAttack;
                @HammerAttack.canceled += instance.OnHammerAttack;
                @Test.started += instance.OnTest;
                @Test.performed += instance.OnTest;
                @Test.canceled += instance.OnTest;
                @Invincible.started += instance.OnInvincible;
                @Invincible.performed += instance.OnInvincible;
                @Invincible.canceled += instance.OnInvincible;
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSwordAttack(InputAction.CallbackContext context);
        void OnHammerAttack(InputAction.CallbackContext context);
        void OnTest(InputAction.CallbackContext context);
        void OnInvincible(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
    }
}
