using UnityEngine;
using UnityEngine.InputSystem;

public class ShowHideDebugTools : MonoBehaviour
{
    [SerializeField] private GameObject debugToolsContainer;
    private GameInput _input;

    void Start()
    {
        _input = new GameInput();
        _input.Enable();
        _input.Debug.OpenCloseDebugMenu.performed += ShowHide; // BOUND TO KEYBOARD F12
    }

    void OnDestroy()
    {
        _input.Debug.OpenCloseDebugMenu.performed -= ShowHide;
        _input.Disable();
    }

    private void ShowHide(InputAction.CallbackContext context) => debugToolsContainer.SetActive(!debugToolsContainer.activeSelf);
}
