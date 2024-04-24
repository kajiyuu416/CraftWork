using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public bool HoldInput;
    public bool ThrowInput;
    public bool ThrowUpInput;
    public bool ThrowDownInput;
    public bool ReSetInput;
    public bool SettingInput;
    public static PlayerInput Instance
    {
        get; private set;
    }

    public void OnHold(InputValue var)
    {
        HoldInput = var.isPressed;
    }

    public void OnThrow(InputValue var)
    {
        ThrowInput = var.isPressed;
    }
    public void OnThrowUp(InputValue var)
    {
        ThrowUpInput = var.isPressed;
    }
    public void OnThrowDown(InputValue var)
    {
        ThrowDownInput = var.isPressed;
    }
    public void OnReSet(InputValue var)
    {
        ReSetInput = var.isPressed;
    }
    public void OnSetting(InputValue var)
    {
        SettingInput = var.isPressed;
    }

}
