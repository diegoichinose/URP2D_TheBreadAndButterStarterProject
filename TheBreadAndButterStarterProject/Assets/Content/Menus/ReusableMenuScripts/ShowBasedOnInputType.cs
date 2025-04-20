using UnityEngine;
using UnityEngine.InputSystem;

public class ShowBasedOnInputType : MonoBehaviour
{
    [SerializeField] private SettingsData _settingsData;
    [SerializeField] private GameObject ToShowOnKeyboard;
    [SerializeField] private GameObject ToShowOnGamepad;
    private InputType selectedInputType;

    void OnEnable() 
    {
        InputDetectionManager.instance.OnInputDeviceChange += TryShowOnInputChange; 
    }

    void OnDisable() 
    {
        InputDetectionManager.instance.OnInputDeviceChange -= TryShowOnInputChange; 
    }

    private void TryShowOnInputChange()
    {
        HideAll();

        if (_settingsData.settings.hideInstructions)
            return;

        if (InputDetectionManager.instance.lastUsedDevice is Keyboard)
            ToShowOnKeyboard.SetActive(true);
        
        if (InputDetectionManager.instance.lastUsedDevice is Gamepad)
            ToShowOnGamepad.SetActive(true);
    }

    private void HideAll()
    {
        ToShowOnKeyboard.SetActive(false);
        ToShowOnGamepad.SetActive(false);
    }

    private void UpdateOnHideInstructionsSettingsChange() => TryShowOnInputChange();
}