//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Controls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Controls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Keyboard"",
            ""id"": ""97c0e09b-a68d-46da-a9a9-597c0a4d9c1e"",
            ""actions"": [
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""238e0e35-2bf4-44cf-a8ea-b4cf4de83e7a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""19289b4a-e9c2-4160-976b-3793ead1f417"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SoftDrop"",
                    ""type"": ""Button"",
                    ""id"": ""1347f6c6-a3c3-432f-82f4-6d6761eff5c9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""HardDrop"",
                    ""type"": ""Button"",
                    ""id"": ""58430e87-673e-46cd-9f3c-74769c60a6e1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RotateLeft"",
                    ""type"": ""Button"",
                    ""id"": ""413a5a94-f43a-4160-86d1-786cae7fb67d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RotateRight"",
                    ""type"": ""Button"",
                    ""id"": ""54934495-34ea-4a62-8c13-d32531e30d60"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Hold"",
                    ""type"": ""Button"",
                    ""id"": ""7841563c-0aa9-4723-bcb2-f5871eed87ad"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""33eddc73-bb82-4847-9195-f4a702a9a223"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skill1"",
                    ""type"": ""Button"",
                    ""id"": ""74f8068f-c257-4fbb-a8ea-c815c12d57ef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skill2"",
                    ""type"": ""Button"",
                    ""id"": ""6a7002b4-8636-4772-b025-466b005e8275"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skill3"",
                    ""type"": ""Button"",
                    ""id"": ""9fcb6768-4d4b-49fc-8246-555363917ae6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skill4"",
                    ""type"": ""Button"",
                    ""id"": ""07959440-e7cd-474d-8090-a5f6ad27112e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skill5"",
                    ""type"": ""Button"",
                    ""id"": ""6cd20864-f089-4ab3-97f5-729116bf6c64"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SkillFinal"",
                    ""type"": ""Button"",
                    ""id"": ""cc0b6284-c03e-4c87-b0cc-64b790d243e0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""65f0020b-aad7-4411-a3f4-1210c8a3370f"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d85739b2-c636-49af-ae94-1524f3f20cbc"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""28d4e761-2e90-49eb-b627-9a52413832ae"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SoftDrop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""28cad60a-a395-4d9f-8115-4df3e3d18935"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HardDrop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f0ee3a20-6ea8-41ef-869f-2c16d8118ddc"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94c79548-8455-4f99-a91c-9345b8557e48"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""648eb6bc-abb7-43c5-844f-f32d16fd8134"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a20e6042-f5d5-43de-9864-25cdbe2824f1"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""324fb5f9-ddf2-4b66-b41b-3df3bee22041"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d0043bce-d252-4195-b007-25d54cd3b09e"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2e0decba-1480-4223-bb33-06ef115d895f"",
                    ""path"": ""<Keyboard>/f1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""48b3f4ac-7f60-460b-812d-0e5b5a91519f"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""61aa8c3b-be0c-41ef-8bb7-74a0a97b1503"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""faaf0d7a-a314-45b6-ad08-7bb2fd029bd0"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8eae1a6-da41-4d5d-8322-ed9fe0c5fffa"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4947c4c7-8b85-4fa7-b91d-1c671cf37b6e"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4bfc188c-a0a0-4c42-9dd6-10e54ea5e008"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkillFinal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5b2cafde-26c6-4738-8349-1d22307d6e94"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Keyboard
        m_Keyboard = asset.FindActionMap("Keyboard", throwIfNotFound: true);
        m_Keyboard_Left = m_Keyboard.FindAction("Left", throwIfNotFound: true);
        m_Keyboard_Right = m_Keyboard.FindAction("Right", throwIfNotFound: true);
        m_Keyboard_SoftDrop = m_Keyboard.FindAction("SoftDrop", throwIfNotFound: true);
        m_Keyboard_HardDrop = m_Keyboard.FindAction("HardDrop", throwIfNotFound: true);
        m_Keyboard_RotateLeft = m_Keyboard.FindAction("RotateLeft", throwIfNotFound: true);
        m_Keyboard_RotateRight = m_Keyboard.FindAction("RotateRight", throwIfNotFound: true);
        m_Keyboard_Hold = m_Keyboard.FindAction("Hold", throwIfNotFound: true);
        m_Keyboard_Pause = m_Keyboard.FindAction("Pause", throwIfNotFound: true);
        m_Keyboard_Skill1 = m_Keyboard.FindAction("Skill1", throwIfNotFound: true);
        m_Keyboard_Skill2 = m_Keyboard.FindAction("Skill2", throwIfNotFound: true);
        m_Keyboard_Skill3 = m_Keyboard.FindAction("Skill3", throwIfNotFound: true);
        m_Keyboard_Skill4 = m_Keyboard.FindAction("Skill4", throwIfNotFound: true);
        m_Keyboard_Skill5 = m_Keyboard.FindAction("Skill5", throwIfNotFound: true);
        m_Keyboard_SkillFinal = m_Keyboard.FindAction("SkillFinal", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Keyboard
    private readonly InputActionMap m_Keyboard;
    private IKeyboardActions m_KeyboardActionsCallbackInterface;
    private readonly InputAction m_Keyboard_Left;
    private readonly InputAction m_Keyboard_Right;
    private readonly InputAction m_Keyboard_SoftDrop;
    private readonly InputAction m_Keyboard_HardDrop;
    private readonly InputAction m_Keyboard_RotateLeft;
    private readonly InputAction m_Keyboard_RotateRight;
    private readonly InputAction m_Keyboard_Hold;
    private readonly InputAction m_Keyboard_Pause;
    private readonly InputAction m_Keyboard_Skill1;
    private readonly InputAction m_Keyboard_Skill2;
    private readonly InputAction m_Keyboard_Skill3;
    private readonly InputAction m_Keyboard_Skill4;
    private readonly InputAction m_Keyboard_Skill5;
    private readonly InputAction m_Keyboard_SkillFinal;
    public struct KeyboardActions
    {
        private @Controls m_Wrapper;
        public KeyboardActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Left => m_Wrapper.m_Keyboard_Left;
        public InputAction @Right => m_Wrapper.m_Keyboard_Right;
        public InputAction @SoftDrop => m_Wrapper.m_Keyboard_SoftDrop;
        public InputAction @HardDrop => m_Wrapper.m_Keyboard_HardDrop;
        public InputAction @RotateLeft => m_Wrapper.m_Keyboard_RotateLeft;
        public InputAction @RotateRight => m_Wrapper.m_Keyboard_RotateRight;
        public InputAction @Hold => m_Wrapper.m_Keyboard_Hold;
        public InputAction @Pause => m_Wrapper.m_Keyboard_Pause;
        public InputAction @Skill1 => m_Wrapper.m_Keyboard_Skill1;
        public InputAction @Skill2 => m_Wrapper.m_Keyboard_Skill2;
        public InputAction @Skill3 => m_Wrapper.m_Keyboard_Skill3;
        public InputAction @Skill4 => m_Wrapper.m_Keyboard_Skill4;
        public InputAction @Skill5 => m_Wrapper.m_Keyboard_Skill5;
        public InputAction @SkillFinal => m_Wrapper.m_Keyboard_SkillFinal;
        public InputActionMap Get() { return m_Wrapper.m_Keyboard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardActions set) { return set.Get(); }
        public void SetCallbacks(IKeyboardActions instance)
        {
            if (m_Wrapper.m_KeyboardActionsCallbackInterface != null)
            {
                @Left.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnLeft;
                @Right.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRight;
                @SoftDrop.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSoftDrop;
                @SoftDrop.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSoftDrop;
                @SoftDrop.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSoftDrop;
                @HardDrop.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnHardDrop;
                @HardDrop.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnHardDrop;
                @HardDrop.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnHardDrop;
                @RotateLeft.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRotateLeft;
                @RotateLeft.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRotateLeft;
                @RotateLeft.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRotateLeft;
                @RotateRight.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRotateRight;
                @RotateRight.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRotateRight;
                @RotateRight.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRotateRight;
                @Hold.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnHold;
                @Hold.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnHold;
                @Hold.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnHold;
                @Pause.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnPause;
                @Skill1.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSkill1;
                @Skill1.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSkill1;
                @Skill1.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSkill1;
                @Skill2.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSkill2;
                @Skill2.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSkill2;
                @Skill2.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSkill2;
                @Skill3.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSkill3;
                @Skill3.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSkill3;
                @Skill3.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSkill3;
                @Skill4.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSkill4;
                @Skill4.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSkill4;
                @Skill4.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSkill4;
                @Skill5.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSkill5;
                @Skill5.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSkill5;
                @Skill5.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSkill5;
                @SkillFinal.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSkillFinal;
                @SkillFinal.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSkillFinal;
                @SkillFinal.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSkillFinal;
            }
            m_Wrapper.m_KeyboardActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
                @SoftDrop.started += instance.OnSoftDrop;
                @SoftDrop.performed += instance.OnSoftDrop;
                @SoftDrop.canceled += instance.OnSoftDrop;
                @HardDrop.started += instance.OnHardDrop;
                @HardDrop.performed += instance.OnHardDrop;
                @HardDrop.canceled += instance.OnHardDrop;
                @RotateLeft.started += instance.OnRotateLeft;
                @RotateLeft.performed += instance.OnRotateLeft;
                @RotateLeft.canceled += instance.OnRotateLeft;
                @RotateRight.started += instance.OnRotateRight;
                @RotateRight.performed += instance.OnRotateRight;
                @RotateRight.canceled += instance.OnRotateRight;
                @Hold.started += instance.OnHold;
                @Hold.performed += instance.OnHold;
                @Hold.canceled += instance.OnHold;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Skill1.started += instance.OnSkill1;
                @Skill1.performed += instance.OnSkill1;
                @Skill1.canceled += instance.OnSkill1;
                @Skill2.started += instance.OnSkill2;
                @Skill2.performed += instance.OnSkill2;
                @Skill2.canceled += instance.OnSkill2;
                @Skill3.started += instance.OnSkill3;
                @Skill3.performed += instance.OnSkill3;
                @Skill3.canceled += instance.OnSkill3;
                @Skill4.started += instance.OnSkill4;
                @Skill4.performed += instance.OnSkill4;
                @Skill4.canceled += instance.OnSkill4;
                @Skill5.started += instance.OnSkill5;
                @Skill5.performed += instance.OnSkill5;
                @Skill5.canceled += instance.OnSkill5;
                @SkillFinal.started += instance.OnSkillFinal;
                @SkillFinal.performed += instance.OnSkillFinal;
                @SkillFinal.canceled += instance.OnSkillFinal;
            }
        }
    }
    public KeyboardActions @Keyboard => new KeyboardActions(this);
    public interface IKeyboardActions
    {
        void OnLeft(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnSoftDrop(InputAction.CallbackContext context);
        void OnHardDrop(InputAction.CallbackContext context);
        void OnRotateLeft(InputAction.CallbackContext context);
        void OnRotateRight(InputAction.CallbackContext context);
        void OnHold(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnSkill1(InputAction.CallbackContext context);
        void OnSkill2(InputAction.CallbackContext context);
        void OnSkill3(InputAction.CallbackContext context);
        void OnSkill4(InputAction.CallbackContext context);
        void OnSkill5(InputAction.CallbackContext context);
        void OnSkillFinal(InputAction.CallbackContext context);
    }
}
