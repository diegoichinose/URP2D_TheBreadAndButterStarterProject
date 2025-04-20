using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OnMovementSelectButton : MonoBehaviour
{
    [SerializeField] private Button selectThisButton;
    private GameInput _input;

    void OnEnable()
    {
        _input = new GameInput();
        _input.Enable();
        _input.Menu.Navigate.performed += SelectThisButton;
    }

    void OnDisable()
    {
        _input.Menu.Navigate.performed -= SelectThisButton;
        _input.Disable();
    }

    private void SelectThisButton(InputAction.CallbackContext context) 
    {
        selectThisButton.Select();
        _input.Menu.Navigate.performed -= SelectThisButton;
    }
}