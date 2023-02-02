using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class ControlsManager : MonoBehaviour,
    Controls.IKeyboardActions
{
    public Controls controls;

    public event EventHandler<OnKeyPressedEventArgs> OnKeyPressed;
    public event EventHandler<OnSkillPressedEventArgs> OnSkillPressed;
    public event EventHandler OnPausePressed;

    public class OnKeyPressedEventArgs : EventArgs {
        public ActionName action;
    }

    public class OnSkillPressedEventArgs : EventArgs {
        public CharacterData.SkillName id;
    }

    // Action name for sending event
    public enum ActionName {
        LeftPressed,
        LeftHeld,
        RightPressed,
        RightHeld,
        HorizontalCancelled,
        SoftDropPressed,
        SoftDropCancelled,
        HardDrop,
        RotateLeft,
        RotateRight,
        Hold,
    }


    private void OnEnable(){
        controls.Keyboard.Enable();
    }

    private void OnDisable(){
        controls.Keyboard.Disable();
    }
    
    private void Awake(){
        controls = new Controls();
        controls.Keyboard.SetCallbacks(this);
        

    }

    
    // Recieving callbacks to sending events

    // public void OnLeft(InputAction.CallbackContext context){
    //     switch (context.phase){
    //         case InputActionPhase.Started :
    //             OnKeyPressed?.Invoke(this, new OnKeyPressedEventArgs { action = ActionName.LeftPressed } );
    //             break;
    //         case InputActionPhase.Performed :
    //             OnKeyPressed?.Invoke(this, new OnKeyPressedEventArgs { action = ActionName.LeftHeld } );
    //             break;
    //         case InputActionPhase.Canceled :
    //             OnKeyPressed?.Invoke(this, new OnKeyPressedEventArgs { action = ActionName.LeftCancelled } );
    //             break;
    //         default :
    //             break;
    //     }
        
    // }

    // public void OnRight(InputAction.CallbackContext context){
    //     switch (context.phase){
    //         case InputActionPhase.Started :
    //             OnKeyPressed?.Invoke(this, new OnKeyPressedEventArgs { action = ActionName.RightPressed } );
    //             break;
    //         case InputActionPhase.Performed :
    //             OnKeyPressed?.Invoke(this, new OnKeyPressedEventArgs { action = ActionName.RightHeld } );
    //             break;
    //         case InputActionPhase.Canceled :
    //             OnKeyPressed?.Invoke(this, new OnKeyPressedEventArgs { action = ActionName.RightCancelled } );
    //             break;
    //         default :
    //             break;
    //     }
    // }

    public void OnSoftDrop(InputAction.CallbackContext context){
        if (context.phase == InputActionPhase.Performed){
            OnKeyPressed?.Invoke(this, new OnKeyPressedEventArgs { action = ActionName.SoftDropPressed } );
        } else if (context.phase == InputActionPhase.Canceled){
            OnKeyPressed?.Invoke(this, new OnKeyPressedEventArgs { action = ActionName.SoftDropCancelled } );
        }
    }

    public void OnHardDrop(InputAction.CallbackContext context){
        if (context.phase == InputActionPhase.Performed){
            OnKeyPressed?.Invoke(this, new OnKeyPressedEventArgs { action = ActionName.HardDrop } );
        }
    }

    public void OnRotateLeft(InputAction.CallbackContext context){
        if (context.phase == InputActionPhase.Performed){
            OnKeyPressed?.Invoke(this, new OnKeyPressedEventArgs { action = ActionName.RotateLeft } );
        }
    }

    public void OnRotateRight(InputAction.CallbackContext context){
        if (context.phase == InputActionPhase.Performed){
            OnKeyPressed?.Invoke(this, new OnKeyPressedEventArgs { action = ActionName.RotateRight } );
        }
    }

    public void OnHold(InputAction.CallbackContext context){
        if (context.phase == InputActionPhase.Performed){
            OnKeyPressed?.Invoke(this, new OnKeyPressedEventArgs { action = ActionName.Hold } );
        }
    }

    public void OnPause(InputAction.CallbackContext context){
        if (context.phase == InputActionPhase.Performed){
            OnPausePressed?.Invoke(this, EventArgs.Empty);
        }
    }

    public void OnSkill1(InputAction.CallbackContext context){
        if (context.phase == InputActionPhase.Performed){
            OnSkillPressed?.Invoke(this, new OnSkillPressedEventArgs { id = CharacterData.SkillName.Skill1 } );
        }
    }

    public void OnSkill2(InputAction.CallbackContext context){
        if (context.phase == InputActionPhase.Performed){
            OnSkillPressed?.Invoke(this, new OnSkillPressedEventArgs { id = CharacterData.SkillName.Skill2 } );
        }
    }

    public void OnSkill3(InputAction.CallbackContext context){
        if (context.phase == InputActionPhase.Performed){
            OnSkillPressed?.Invoke(this, new OnSkillPressedEventArgs { id = CharacterData.SkillName.Skill3 } );
        }
    }

    public void OnSkill4(InputAction.CallbackContext context){
        if (context.phase == InputActionPhase.Performed){
            OnSkillPressed?.Invoke(this, new OnSkillPressedEventArgs { id = CharacterData.SkillName.Skill4 } );
        }
    }

    public void OnSkill5(InputAction.CallbackContext context){
        if (context.phase == InputActionPhase.Performed){
            OnSkillPressed?.Invoke(this, new OnSkillPressedEventArgs { id = CharacterData.SkillName.Skill5 } );
        }
    }

    public void OnSkillFinal(InputAction.CallbackContext context){  
        if (context.phase == InputActionPhase.Performed){
            OnSkillPressed?.Invoke(this, new OnSkillPressedEventArgs { id = CharacterData.SkillName.SkillFinal } );
        }
    }

    public void OnHorizontalMovement(InputAction.CallbackContext context){
        float value = context.ReadValue<float>();

        if (context.phase == InputActionPhase.Started){
            if (value == 1){
                OnKeyPressed?.Invoke(this, new OnKeyPressedEventArgs { action = ActionName.RightPressed } );
            } else if (value == -1){
                OnKeyPressed?.Invoke(this, new OnKeyPressedEventArgs { action = ActionName.LeftPressed } );
            }
        }

        if (context.phase == InputActionPhase.Performed){
            if (value == 1){
                OnKeyPressed?.Invoke(this, new OnKeyPressedEventArgs { action = ActionName.RightHeld } );
            } else if (value == -1){
                OnKeyPressed?.Invoke(this, new OnKeyPressedEventArgs { action = ActionName.LeftHeld } );
            }
        }

        if (context.phase == InputActionPhase.Canceled){
            OnKeyPressed?.Invoke(this, new OnKeyPressedEventArgs { action = ActionName.HorizontalCancelled } );
        }
    }
}
