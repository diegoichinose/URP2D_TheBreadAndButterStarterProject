using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnableDisableGameObjectsBasedOnInputDevice : MonoBehaviour
{
    [SerializeField] private List<GameObject> enableTheseForGamepad;
    [SerializeField] private List<GameObject> enableTheseForKeyboard;

    void OnEnable()
    {
        ShowGameObjectsBasedOnLastUsedDevice();
        InputDetectionManager.instance.OnInputDeviceChange += ShowGameObjectsBasedOnLastUsedDevice; 
    }

    void OnDisable()
    {
        InputDetectionManager.instance.OnInputDeviceChange -= ShowGameObjectsBasedOnLastUsedDevice; 
    }

    private void ShowGameObjectsBasedOnLastUsedDevice()
    {
        enableTheseForGamepad.ForEach(navText => navText.SetActive(false));
        enableTheseForKeyboard.ForEach(navText => navText.SetActive(false));

        if (InputDetectionManager.instance.lastUsedDevice is Gamepad)
            enableTheseForGamepad.ForEach(navText => navText.SetActive(true));
        else
            enableTheseForKeyboard.ForEach(navText => navText.SetActive(true));
    }
}