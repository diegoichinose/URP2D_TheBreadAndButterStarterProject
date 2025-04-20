using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class InputDetectionManager : MonoBehaviour
{
    [SerializeField] private InputIconData gamepadIconData;
    [SerializeField] private InputIconData keyboardIconData;
    public static InputDetectionManager instance = null;
    public InputIconData currentIconData;	
    public InputDevice lastUsedDevice;
    public Action OnInputDeviceChange;
    
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void OnEnable()
    {
        InputSystem.onEvent += OnDeviceChange;
    }

    void OnDisable()
    {
        InputSystem.onEvent -= OnDeviceChange;
    }

    private void OnDeviceChange(InputEventPtr eventPtr, InputDevice currentDevice)
    {
        if (lastUsedDevice == currentDevice)
            return;

        // Some devices like to spam events like crazy.
        // Make sure you close STEAM and other similar controller binding programs.
        // Example: PS4 controller on PC keeps triggering events without meaningful change.
        // To fix this we go through the changed controls in the event and look for ones actuated above a magnitude of a little above zero.
        if (eventPtr.type == StateEvent.Type)
            if (!eventPtr.EnumerateChangedControls(currentDevice, 0.0001f).Any())
                return;

        lastUsedDevice = currentDevice;

        if (lastUsedDevice is Gamepad) 
            OnGamepadSelected();
        else 
            OnKeyboardSelected();
    }

    private void OnGamepadSelected()
    {
        currentIconData = gamepadIconData;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        OnInputDeviceChange?.Invoke();
    }

    private void OnKeyboardSelected()
    {
        currentIconData = keyboardIconData;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        OnInputDeviceChange?.Invoke();
    }
}

public enum InputType
{
    Keyboard,
    Gamepad,
    None
}