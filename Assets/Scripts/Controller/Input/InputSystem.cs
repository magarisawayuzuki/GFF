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
                    ""name"": ""invincible"",
                    ""type"": ""Button"",
                    ""id"": ""7e9fa58d-3bef-4f1d-8248-44a43c20b886"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftEvasion"",
                    ""type"": ""Button"",
                    ""id"": ""3f1118d1-96a4-4787-b27a-aeefe5ad3863"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightEvasion"",
                    ""type"": ""Button"",
                    ""id"": ""25d263de-4eaa-4a99-9760-8e30b5b36bd7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""A,D"",
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
                    ""groups"": """",
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
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow"",
                    ""id"": ""6869159a-315d-4e53-ad22-f7ce59fd186d"",
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
                    ""id"": ""9fccb612-3675-462a-970d-11ab741ed027"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""52d6ba6b-22af-4c57-aa75-74da12bf3338"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""XBox"",
                    ""id"": ""6e0e7489-7ba6-4707-9506-7ebf20a40027"",
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
                    ""id"": ""b6f48f1d-9e09-42c7-af52-13ec48e37545"",
                    ""path"": ""<XInputController>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""20215eb6-b4f9-4e7c-80bb-9fab3e130fa9"",
                    ""path"": ""<XInputController>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
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
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""958f6281-cba9-4b21-8001-ac27d4649b08"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
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
                    ""groups"": """",
                    ""action"": ""SwordAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3bcac4a6-90bf-45a6-b07e-392dc4a73850"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
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
                    ""groups"": """",
                    ""action"": ""HammerAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""72ddeefd-0810-4c8a-afed-a974cce5939e"",
                    ""path"": ""<XInputController>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HammerAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bc2f9cdb-ba4c-4508-ac5d-4da19e9d8777"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""invincible"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""XBOX"",
                    ""id"": ""dc161b5c-39f7-41a9-aad3-c0a12d734b7a"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""invincible"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""a1755c8b-4344-4886-bc90-da898ae41ec8"",
                    ""path"": ""<XInputController>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""invincible"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""9b3df8af-f04d-4aae-baec-4ea20ca6e805"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""invincible"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b5974226-e0ba-4d63-bc1d-bbcd44775b5d"",
                    ""path"": ""<XInputController>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftEvasion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""KeyBord"",
                    ""id"": ""202cf389-f213-4112-97d5-0678f1b9d59e"",
                    ""path"": ""ButtonWithTwoModifiers"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftEvasion"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier1"",
                    ""id"": ""3798d218-96ea-4428-80f6-02cd178bc18b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftEvasion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""modifier2"",
                    ""id"": ""75b7646c-0e3b-431b-b32d-5dee73c91fed"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftEvasion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""fecd7ad2-0466-4d00-a6fc-781546c11706"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftEvasion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""89094cef-1fcf-4b0a-95b9-e252630b02e2"",
                    ""path"": ""<XInputController>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightEvasion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""KeyBord"",
                    ""id"": ""3dab03b9-d980-4288-a3c9-e64aaede6f3b"",
                    ""path"": ""ButtonWithTwoModifiers"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightEvasion"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier1"",
                    ""id"": ""35de5744-6a94-4458-acea-d73b40ace59c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightEvasion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""modifier2"",
                    ""id"": ""68f46a92-eb86-42e1-b237-29416c80e88a"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightEvasion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""2e68a55a-3b57-4e4a-81db-155104105f90"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightEvasion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_SwordAttack = m_Player.FindAction("SwordAttack", throwIfNotFound: true);
        m_Player_HammerAttack = m_Player.FindAction("HammerAttack", throwIfNotFound: true);
        m_Player_invincible = m_Player.FindAction("invincible", throwIfNotFound: true);
        m_Player_LeftEvasion = m_Player.FindAction("LeftEvasion", throwIfNotFound: true);
        m_Player_RightEvasion = m_Player.FindAction("RightEvasion", throwIfNotFound: true);
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
    private readonly InputAction m_Player_invincible;
    private readonly InputAction m_Player_LeftEvasion;
    private readonly InputAction m_Player_RightEvasion;
    public struct PlayerActions
    {
        private @InputSystem m_Wrapper;
        public PlayerActions(@InputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @SwordAttack => m_Wrapper.m_Player_SwordAttack;
        public InputAction @HammerAttack => m_Wrapper.m_Player_HammerAttack;
        public InputAction @invincible => m_Wrapper.m_Player_invincible;
        public InputAction @LeftEvasion => m_Wrapper.m_Player_LeftEvasion;
        public InputAction @RightEvasion => m_Wrapper.m_Player_RightEvasion;
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
                @invincible.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInvincible;
                @invincible.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInvincible;
                @invincible.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInvincible;
                @LeftEvasion.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftEvasion;
                @LeftEvasion.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftEvasion;
                @LeftEvasion.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftEvasion;
                @RightEvasion.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightEvasion;
                @RightEvasion.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightEvasion;
                @RightEvasion.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightEvasion;
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
                @invincible.started += instance.OnInvincible;
                @invincible.performed += instance.OnInvincible;
                @invincible.canceled += instance.OnInvincible;
                @LeftEvasion.started += instance.OnLeftEvasion;
                @LeftEvasion.performed += instance.OnLeftEvasion;
                @LeftEvasion.canceled += instance.OnLeftEvasion;
                @RightEvasion.started += instance.OnRightEvasion;
                @RightEvasion.performed += instance.OnRightEvasion;
                @RightEvasion.canceled += instance.OnRightEvasion;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSwordAttack(InputAction.CallbackContext context);
        void OnHammerAttack(InputAction.CallbackContext context);
        void OnInvincible(InputAction.CallbackContext context);
        void OnLeftEvasion(InputAction.CallbackContext context);
        void OnRightEvasion(InputAction.CallbackContext context);
    }
}
