using UnityEngine;
using UnityEngine.InputSystem;

public class OnEnableSubscribeInputsToDisableSelf : MonoBehaviour
{
    private GameInput _playerInput;

    [Header("ALLOW DISABLE SELF WITH")]
    [SerializeField] private bool rightClickInput;
    [SerializeField] private bool cancelInput;
    [SerializeField] private bool escInput;
    private void DisableSelf(InputAction.CallbackContext obj) => gameObject.SetActive(false);

    void OnEnable()
    {
        _playerInput = new GameInput();
        _playerInput.Enable();
        _playerInput.Menu.CloseMenu.performed += DisableSelf;
    }

    void OnDisable()
    {
        _playerInput.Menu.CloseMenu.performed -= DisableSelf;
        _playerInput.Disable();
    }
}
